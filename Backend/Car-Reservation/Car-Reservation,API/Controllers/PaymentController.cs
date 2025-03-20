using Car_Reservation.APIs.Controllers;
using Car_Reservation.Dtos.PaymentDtos;
using Car_Reservation.DTOs.ErrorResponse;
using Car_Reservation_Domain.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Car_Reservation_API.Controllers;

public class PaymentController(IPaymentService paymentService) : BaseApiController
{
    private readonly IPaymentService paymentService = paymentService;

    [HttpPost("create-checkout-session")]
    [ProducesResponseType(typeof(CheckoutSessionToReturnDto),200)]
    [ProducesResponseType(typeof(ApiResponse),404)]
    [ProducesResponseType(typeof(ApiResponse),403)]
    [ProducesResponseType(typeof(ApiResponse),409)]
    [ProducesResponseType(typeof(ApiResponse),422)]
    [ProducesResponseType(typeof(ApiExceptionErrorResponse),500)]
    [Authorize]
    public async Task<IActionResult> CreateCheckoutSession(CheckoutSessionDto dto )
    {
       var userEmail =  User.FindFirst(ClaimTypes.Email)!;
       var result = await paymentService.CreateCheckoutSession(dto.ReservationId, dto.SuccessUrl, dto.CancelUrl,userEmail.Value);

        switch (result.Status)
        {
            case 200:
                return Ok(new CheckoutSessionToReturnDto() { CheckoutSessionUrl = result.SuccessUrl!});
            case 500:
                return StatusCode(result.Status, new ApiExceptionErrorResponse(result.ErrorMassage!));
            default:
                return StatusCode(result.Status, new ApiResponse(result.Status, result.ErrorMassage!));
        }
    }

    [HttpPost("/webhook")]
    public async Task<IActionResult> HandleStripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

       
        await paymentService.HandleStripeWebhookAsync(json);
        return Ok();
    }

    
}