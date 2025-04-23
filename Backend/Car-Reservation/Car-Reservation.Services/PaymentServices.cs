using Car_Reservation.Repository.Specfications.ReservationSpecification;
using Car_Reservation.Repository.UnitOfWork;
using Car_Reservation_Domain.Entities.EmailEntity;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.ServicesInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;
using Car_Reservation_Domain.Entities.CarEntity;


namespace Car_Reservation.Services;

public class StripePaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly ISendEmail _emailService;
    private readonly ILogger<StripePaymentService> _logger;
    private readonly string _stripeApiKey;

    public StripePaymentService(
        IUnitOfWork unitOfWork,
        IConfiguration configuration,
        ISendEmail emailService,
        ILogger<StripePaymentService> logger)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _emailService = emailService;
        _logger = logger;
        _stripeApiKey = _configuration["Stripe:Secretkey"]!;
    }

    public async Task<CreateCheckoutSessionResponse> CreateCheckoutSession(int reservationId, string? successUrl, string? cancelUrl, string userEmail)
    {
        try
        {
            if (string.IsNullOrEmpty(_stripeApiKey))
            {
                _logger.LogError("Stripe API key is not configured");
                return new CreateCheckoutSessionResponse()
                {
                    Status = 500,
                    ErrorMassage = "Payment service configuration error",
                };
            }

            // Set the success and cancel URLs with fallbacks
            var finalSuccessUrl = !string.IsNullOrEmpty(successUrl)
                ? successUrl
                : _configuration["Stripe:SuccessUrl"] ?? "https://example.com/success";

            var finalCancelUrl = !string.IsNullOrEmpty(cancelUrl)
                ? cancelUrl
                : _configuration["Stripe:CancelUrl"] ?? "https://example.com/cancel";

            StripeConfiguration.ApiKey = _stripeApiKey;

            // Get the reservation with related data
            var reservationSpec = new ReservationSpec(r => r.Id == reservationId);
            var reservation = await _unitOfWork.Repository<Reservation>().GetAsyncWithSpecification(reservationSpec);

            if (reservation == null)
            {
                return new CreateCheckoutSessionResponse()
                {
                    Status = 404,
                    ErrorMassage = "Reservation not found."
                };
            }

            if (reservation.Car == null)
            {
                return new CreateCheckoutSessionResponse()
                {
                    Status = 422, // Unprocessable Entity
                    ErrorMassage = "This reservation has no associated Car."
                };
            }

            if (reservation.User == null)
            {
                return new CreateCheckoutSessionResponse()
                {
                    Status = 422, // Unprocessable Entity
                    ErrorMassage = "This reservation has no associated user."
                };
            }

            if (reservation.User.Email != userEmail)
            {
                return new CreateCheckoutSessionResponse()
                {
                    Status = 403, // Forbidden
                    ErrorMassage = "You are not authorized to pay for this reservation."
                };
            }

            if (reservation.Status == ReservationStatus.Confirmed)
            {
                return new CreateCheckoutSessionResponse()
                {
                    Status = 409, // Conflict
                    ErrorMassage = "This reservation has already been confirmed and paid for."
                };
            }

            var carName = (await _unitOfWork.Repository<Model>().GetAsync(reservation.Car.ModelId))!.Name;

            // Calculate the rental period in days
            int rentalDays = (int)(reservation.EndDate - reservation.StartDate).TotalDays + 1; // +1 to include both start and end days

            // Calculate total amount in cents
            var dailyRate = reservation.Car.Price;
            var totalAmount = (long)((dailyRate * rentalDays + reservation.Car.InsuranceCost) * 100) ; // Convert to cents

            var options = new SessionCreateOptions
            {
                CustomerEmail = reservation.User.Email,
                Currency = "USD",
                SuccessUrl = finalSuccessUrl,
                CancelUrl = finalCancelUrl,
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            UnitAmount = totalAmount,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = carName,
                                Description = $"Reservation #{reservationId} - {reservation.StartDate:MM/dd/yyyy} to {reservation.EndDate:MM/dd/yyyy} ({rentalDays} days)"
                            }
                        },
                        Quantity = 1
                    },
                },
                Mode = "payment",
                Metadata = new Dictionary<string, string>
                {
                    { "reservationId", reservationId.ToString() }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            // Update reservation status
            reservation.PaymentIntentId = session.PaymentIntentId;
            reservation.Status = ReservationStatus.PaymentPending;
            await _unitOfWork.CompleteAsync();

            // Send payment email
            if (!string.IsNullOrEmpty(reservation.User.Email))
            {
                try
                {
                    await SendPaymentLinkEmailAsync(reservation, session.Url);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send payment email to {Email}", reservation.User.Email);
                }
            }

            return new CreateCheckoutSessionResponse()
            {
                Status = 200,
                SuccessUrl = session.Url
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating checkout session for reservation {ReservationId}", reservationId);
            return new CreateCheckoutSessionResponse()
            {
                Status = 500,
                ErrorMassage = "An error occurred while processing your payment request"
            };
        }
    }

    public async Task<int> HandleStripeWebhookAsync(string? json)
    {
        try
        {
            // Validate webhook signature
            var stripeEvent = EventUtility.ParseEvent(json);

            // Handle the event based on its type
            switch (stripeEvent.Type)
            {
                case EventTypes.CheckoutSessionCompleted:
                    await HandleCheckoutSessionEvent(stripeEvent, session =>
                        session.PaymentStatus == "paid" ? ReservationStatus.Confirmed : ReservationStatus.PaymentPending);
                    break;

                case EventTypes.CheckoutSessionAsyncPaymentSucceeded:
                    await HandleCheckoutSessionEvent(stripeEvent, _ => ReservationStatus.Confirmed);
                    break;

                case EventTypes.CheckoutSessionAsyncPaymentFailed:
                    await HandleCheckoutSessionEvent(stripeEvent, _ => ReservationStatus.PaymentFailed);
                    break;

                case EventTypes.CheckoutSessionExpired:
                    await HandleCheckoutSessionEvent(stripeEvent, _ => ReservationStatus.PaymentFailed);
                    break;

                default:
                    _logger.LogInformation("Unhandled event type: {EventType}", stripeEvent.Type);
                    break;
            }

            return 200;
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Error processing Stripe webhook");
            return 400;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error processing webhook");
            return 500;
        }
    }

    public async Task<int> CheckoutSessionFailed(int reservationId, string userEmail)
    {
        var reservationSpec = new ReservationSpec(r => r.Id == reservationId);
        var reservation = await _unitOfWork.Repository<Reservation>().GetAsyncWithSpecification(reservationSpec);
        if (reservation == null)
            return 404;

        if (reservation.User.Email != userEmail)
            return 409;

        reservation.Status = ReservationStatus.PaymentFailed;

        _unitOfWork.Repository<Reservation>().Update(reservation);
        await _unitOfWork.CompleteAsync();
        return 200;
    }

    private async Task HandleCheckoutSessionEvent(Event stripeEvent, Func<Session, ReservationStatus> statusResolver)
    {
        var session = stripeEvent.Data.Object as Session;
        if (session != null && session.Metadata.TryGetValue("reservationId", out string? reservationIdStr) &&
            int.TryParse(reservationIdStr, out int reservationId))
        {
            var reservation = await GetReservationAsync(reservationId);
            if (reservation != null)
            {
                reservation.PaymentIntentId = session.PaymentIntentId;
                reservation.Status = statusResolver(session);
                await UpdateReservationAsync(reservation);

                // Send confirmation email if payment is successful
                if (reservation.Status == ReservationStatus.Confirmed)
                {
                    await _emailService.SendConfirmationEmailAsync(reservation, reservation.User);
                }

                _logger.LogInformation("Checkout session event handled for reservation {ReservationId}, payment status: {PaymentStatus}, new status: {ReservationStatus}",
                    reservationId, session.PaymentStatus, reservation.Status);
            }
        }
    }

    private async Task<Reservation?> GetReservationAsync(int reservationId)
    {
        var reservationSpec = new ReservationSpec(r => r.Id == reservationId);
        return await _unitOfWork.Repository<Reservation>().GetAsyncWithSpecification(reservationSpec);
    }

    private async Task UpdateReservationAsync(Reservation reservation)
    {
        _unitOfWork.Repository<Reservation>().Update(reservation);
        await _unitOfWork.CompleteAsync();
    }

    private async Task SendPaymentLinkEmailAsync(Reservation reservation, string paymentUrl)
    {
        if (reservation == null || string.IsNullOrEmpty(reservation.User.Email) || string.IsNullOrEmpty(paymentUrl))
        {
            throw new ArgumentException("Reservation, user email, and payment URL are required");
        }

        // Calculate rental days
        int rentalDays = (int)(reservation.EndDate - reservation.StartDate).TotalDays + 1;

        // Calculate total cost
        decimal totalCost = (reservation.Car.Price) * rentalDays + reservation.Car.InsuranceCost;

        // Create HTML email template for payment link
        string emailBody = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset=""utf-8"">
                <meta Name=""viewport"" content=""width=device-width, initial-scale=1"">
                <title>Complete Your Car Reservation Payment</title>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                    .header {{ background-color: #1a73e8; color: white; padding: 20px; text-align: center; }}
                    .content {{ padding: 20px; background-color: #f9f9f9; }}
                    .footer {{ text-align: center; padding: 10px; font-size: 12px; color: #777; }}
                    .button {{ background-color: #1a73e8; color: white; padding: 10px 20px; text-decoration: none; border-radius: 4px; display: inline-block; margin: 20px 0; }}
                    table {{ width: 100%; border-collapse: collapse; margin: 20px 0; }}
                    table, th, td {{ border: 1px solid #ddd; }}
                    th, td {{ padding: 10px; text-align: left; }}
                    th {{ background-color: #f2f2f2; }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Complete Your Car Reservation Payment</h1>
                    </div>
                    <div class=""content"">
                        <p>Hello {reservation.User.FName} {reservation.User.LName},</p>
                        <p>Thank you for your reservation (ID: {reservation.Id}). To confirm your booking, please complete the payment by clicking the button below:</p>
                        
                        <table>
                            <tr>
                                <th>Car Model</th>
                                <td>{reservation.Car.Model.Name}</td>
                            </tr>
                            <tr>
                                <th>Pickup Date</th>
                                <td>{reservation.StartDate:MMMM dd, yyyy}</td>
                            </tr>
                            <tr>
                                <th>Return Date</th>
                                <td>{reservation.EndDate:MMMM dd, yyyy}</td>
                            </tr>
                            <tr>
                                <th>Duration</th>
                                <td>{rentalDays} days</td>
                            </tr>
                            <tr>
                                <th>Daily Rate</th>
                                <td>${(reservation.Car.Price):0.00} (Car: ${reservation.Car.Price:0.00} + Insurance: ${reservation.Car.InsuranceCost:0.00})</td>
                            </tr>
                            <tr>
                                <th>Total Cost</th>
                                <td>${totalCost:0.00}</td>
                            </tr>
                        </table>
                        
                        <div style=""text-align: center; margin: 30px 0;"">
                            <a href=""{paymentUrl}"" style=""background-color: #1a73e8; color: white; padding: 12px 20px; text-decoration: none; border-radius: 4px; font-weight: bold;"">Complete Payment</a>
                        </div>
                        
                        <p>Please note that this payment link will expire in 24 hours.</p>
                        <p>If you have any questions, please contact our customer support.</p>
                    </div>
                    <div class=""footer"">
                        <p>&copy; {DateTime.Now.Year} Car Reservation System. All rights reserved.</p>
                        <p>This email was sent to {reservation.User.Email}. If you believe this was sent in error, please contact support.</p>
                    </div>
                </div>
            </body>
            </html>";

        // Create email entity
        var email = new Email
        {
            To = reservation.User.Email,
            Name = $"{reservation.User.FName} {reservation.User.LName}",
            Subject = "Complete Your Car Reservation Payment",
            Body = emailBody
        };

        // Send email using the email service
        await _emailService.SendAsync(email);
    }
}