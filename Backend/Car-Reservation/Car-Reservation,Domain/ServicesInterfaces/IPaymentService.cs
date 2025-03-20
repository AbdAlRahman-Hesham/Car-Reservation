using Car_Reservation_Domain.Entities;

namespace Car_Reservation_Domain.ServicesInterfaces;

public interface IPaymentService
{
    public Task<CreateCheckoutSessionResponse> CreateCheckoutSession(int reservationId, string? successUrl, string? cancelUrl, string userEmail);

    public Task<int> HandleStripeWebhookAsync(string? json);
}

public class CreateCheckoutSessionResponse
{
    public int Status { get; set; }
    public string? ErrorMassage { get; set; }
    public string? SuccessUrl { get; set; }
}