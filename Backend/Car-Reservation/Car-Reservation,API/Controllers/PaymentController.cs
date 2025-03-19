using Car_Reservation.APIs.Controllers;
using Car_Reservation.Repository.Specfications.ReservationSpecification;
using Car_Reservation.Repository.UnitOfWork;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.EmailEntity;
using Car_Reservation_Domain.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
namespace Car_Reservation_API.Controllers;

public class PaymentController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly ISendEmail _emailService;
    private readonly ILogger<PaymentController> _logger;
    private readonly string _stripeApiKey;

    public PaymentController(
        IUnitOfWork unitOfWork,
        IConfiguration configuration,
        ISendEmail emailService,
        ILogger<PaymentController> logger)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _emailService = emailService;
        _logger = logger;
        _stripeApiKey = _configuration["Stripe:Secretkey"]!;
    }

    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession(int reservationId, string successUrl, string cancelUrl)
    {
        try
        {
            if (string.IsNullOrEmpty(_stripeApiKey))
            {
                _logger.LogError("Stripe API key is not configured");
                return StatusCode(500, new { message = "Payment service configuration error" });
            }

            // Set the success and cancel URLs with fallbacks
            var finalSuccessUrl = !string.IsNullOrEmpty(successUrl)
                ? successUrl
                : _configuration["Stripe:SuccessUrl"] ?? "https://yourwebsite.com/payment/success";

            var finalCancelUrl = !string.IsNullOrEmpty(cancelUrl)
                ? cancelUrl
                : _configuration["Stripe:CancelUrl"] ?? "https://yourwebsite.com/payment/cancel";

            StripeConfiguration.ApiKey = _stripeApiKey;

            // Get the reservation with related data
            var reservationSpec = new ReservationSpec(r => r.Id == reservationId);
            var reservation = await _unitOfWork.Repository<Reservation>().GetAsyncWithSpecification(reservationSpec);

            if (reservation == null)
            {
                return NotFound(new { message = "Reservation not found" });
            }

            if (reservation.car == null)
            {
                return BadRequest(new { message = "Reservation has no associated car" });
            }

            if (reservation.User == null)
            {
                return BadRequest(new { message = "Reservation has no associated user" });
            }

            var carName = !string.IsNullOrEmpty(reservation.car.Name) ? reservation.car.Name : "Car Reservation";
            var totalAmount = (long)((reservation.car.Price + reservation.car.InsuranceCost) * 100); // Convert to cents

            var options = new SessionCreateOptions
            {
                CustomerEmail = reservation.User.Email,
                Currency = "USD",
                SuccessUrl = $"https://example.com/success",
                CancelUrl = $"https://example.com/cancel",
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
                                Description = $"Reservation #{reservationId} - {reservation.StartDate:MM/dd/yyyy} to {reservation.EndDate:MM/dd/yyyy}"
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

            return Ok(new
            {
                checkoutUrl = session.Url,
                sessionId = session.Id,
                paymentIntentId = session.PaymentIntentId
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating checkout session for reservation {ReservationId}", reservationId);
            return StatusCode(500, new { message = "An error occurred while processing your payment request" });
        }
    }

    [HttpPost("/webhook")]
    public async Task<IActionResult> HandleStripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ParseEvent(
                json
            );

            // Handle the event based on its type
            if (stripeEvent.Data.Object is PaymentIntent paymentIntent)
            {
                var reservationSpec = new ReservationSpec(r => r.PaymentIntentId == paymentIntent.Id);
                var reservation = await _unitOfWork.Repository<Reservation>().GetAsyncWithSpecification(reservationSpec);

                if (reservation == null)
                {
                    _logger.LogWarning("Webhook received for unknown reservation with PaymentIntent {PaymentIntentId}", paymentIntent.Id);
                    return Ok();
                }

                switch (stripeEvent.Type)
                {
                    case EventTypes.PaymentIntentSucceeded:
                        reservation.Status = ReservationStatus.Confirmed;
                        _logger.LogInformation("Payment succeeded for reservation {ReservationId}", reservation.Id);
                        await _unitOfWork.CompleteAsync();
                        await _emailService.SendConfirmationEmailAsync(reservation, reservation.User);
                        break;

                    case EventTypes.PaymentIntentPaymentFailed:
                        reservation.Status = ReservationStatus.PaymentFailed;
                        _logger.LogWarning("Payment failed for reservation {ReservationId}", reservation.Id);
                        await _unitOfWork.CompleteAsync();
                        break;

                    case EventTypes.PaymentIntentCanceled:
                        reservation.Status = ReservationStatus.Cancelled;
                        _logger.LogInformation("Payment canceled for reservation {ReservationId}", reservation.Id);
                        await _unitOfWork.CompleteAsync();
                        break;
                    case EventTypes.CheckoutSessionCompleted:


                    default:
                        _logger.LogInformation("Unhandled event type: {EventType}", stripeEvent.Type);
                        break;
                }
            }
            else if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                if (session != null && session.Metadata.TryGetValue("reservationId", out string reservationIdStr) &&
                    int.TryParse(reservationIdStr, out int reservationId))
                {
                    var reservationSpec = new ReservationSpec(r => r.Id == reservationId);
                    var reservation = await _unitOfWork.Repository<Reservation>().GetAsyncWithSpecification(reservationSpec);

                    reservation.PaymentIntentId = (stripeEvent.Data.Object as PaymentIntent).Id;
                    _unitOfWork.Repository<Reservation>().Update(reservation);
                    _unitOfWork.CompleteAsync();

                    if (reservation != null)
                    {
                        reservation.Status = session.PaymentStatus == "paid" ?
                            ReservationStatus.Confirmed : ReservationStatus.PaymentPending;

                        await _unitOfWork.CompleteAsync();

                        // Send confirmation email if payment is complete
                        if (session.PaymentStatus == "paid")
                        {
                            await _emailService.SendConfirmationEmailAsync(reservation, reservation.User);
                        }

                        _logger.LogInformation("Checkout session completed for reservation {ReservationId}, payment status: {PaymentStatus}",
                            reservationId, session.PaymentStatus);
                    }
                }
            }

            return Ok();
        }
        catch (StripeException e)
        {
            _logger.LogError(e, "Error processing Stripe webhook");
            return BadRequest();
        }
    }

    private async Task SendPaymentLinkEmailAsync(Reservation reservation, string paymentUrl)
    {
        if (reservation == null || string.IsNullOrEmpty(reservation.User.Email) || string.IsNullOrEmpty(paymentUrl))
        {
            throw new ArgumentException("Reservation, user email, and payment URL are required");
        }

        // Create HTML email template for payment link
        string emailBody = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset=""utf-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
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
                                <td>{reservation.car.Name}</td>
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
                                <th>Total Cost</th>
                                <td>${(reservation.car.Price + reservation.car.InsuranceCost):0.00}</td>
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