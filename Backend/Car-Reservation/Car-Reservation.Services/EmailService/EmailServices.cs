using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.EmailEntity;
using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.ServicesInterfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Car_Reservation.Services.EmailService;

public class EmailServices : ISendEmail
{
    private readonly EmailSettings _options;

    public EmailServices(IOptions<EmailSettings> options)
    {
        _options = options.Value;
    }

    public async Task SendAsync(Email email)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_options.UserName, _options.Email));
        message.To.Add(new MailboxAddress(email.Name, email.To));
        message.Subject = email.Subject;
        message.Body = new TextPart("html")
        {
            Text = email.Body
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_options.Server, _options.Port, false);
            await client.AuthenticateAsync(_options.Email, _options.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

    public async Task SendConfirmationEmailAsync(Reservation reservation, User user)
    {
        var emailTemplate = """
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset="utf-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1">
                    <title>Car Reservation Confirmation</title>
                    <style>
                        body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; }
                        .container { max-width: 600px; margin: 0 auto; padding: 20px; }
                        .header { background-color: #1a73e8; color: white; padding: 20px; text-align: center; }
                        .content { padding: 20px; background-color: #f9f9f9; }
                        .footer { text-align: center; padding: 10px; font-size: 12px; color: #777; }
                        .button { background-color: #1a73e8; color: white; padding: 10px 20px; text-decoration: none; border-radius: 4px; display: inline-block; margin: 20px 0; }
                        table { width: 100%; border-collapse: collapse; margin: 20px 0; }
                        table, th, td { border: 1px solid #ddd; }
                        th, td { padding: 10px; text-align: left; }
                        th { background-color: #f2f2f2; }
                    </style>
                </head>
                <body>
                    <div class="container">
                        <div class="header">
                            <h1>Car Reservation Confirmation</h1>
                        </div>
                        <div class="content">
                            <p>Hello {UserName},</p>
                            <p>Your car reservation has been confirmed. Here are the details:</p>
                            <table>
                                <tr>
                                    <th>Reservation ID</th>
                                    <td>{ReservationId}</td>
                                </tr>
                                <tr>
                                    <th>Car Model</th>
                                    <td>{CarModel}</td>
                                </tr>
                                <tr>
                                    <th>Pickup Date</th>
                                    <td>{PickupDate}</td>
                                </tr>
                                <tr>
                                    <th>Return Date</th>
                                    <td>{ReturnDate}</td>
                                </tr>
                                <tr>
                                    <th>Total Cost</th>
                                    <td>${TotalCost}</td>
                                </tr>
                            </table>
                            <p>If you need to make any changes to your reservation, please contact our customer support team or visit our website.</p>
                            <a href="{ReservationLink}" class="button">View Reservation</a>
                            <p>Thank you for choosing our service!</p>
                        </div>
                        <div class="footer">
                            <p>&copy; {CurrentYear} Car Reservation System. All rights reserved.</p>
                            <p>This email was sent to {UserEmail}. If you believe this was sent in error, please contact support.</p>
                        </div>
                    </div>
                </body>
                </html>
                """;

        // Replace placeholders with actual data
        var emailBody = emailTemplate
            .Replace("{UserName}", $"{user.FName} {user.LName}")
            .Replace("{ReservationId}", reservation.Id.ToString())
            .Replace("{CarModel}", reservation.Car.Model.Name)
            .Replace("{PickupDate}", reservation.StartDate.ToString("MMMM dd, yyyy"))
            .Replace("{ReturnDate}", reservation.EndDate.ToString("MMMM dd, yyyy"))
            .Replace("{TotalCost}", (reservation.Car.InsuranceCost + reservation.Car.Price).ToString("0.00"))
            .Replace("{ReservationLink}", $"https://yourwebsite.com/reservations/{reservation.Id}")
            .Replace("{CurrentYear}", DateTime.Now.Year.ToString())
            .Replace("{UserEmail}", user.Email);

        var email = new Email
        {
            To = user.Email!,
            Name = $"{user.FName} {user.LName}",
            Subject = "Car Reservation Confirmation",
            Body = emailBody
        };

        await SendAsync(email);
    }
}