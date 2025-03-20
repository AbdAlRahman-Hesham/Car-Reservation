using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Car_Reservation.Dtos.PaymentDtos;

public class CheckoutSessionDto
{
    public int ReservationId { get; set; }
    
    [RegularExpression("^(https?|ftp):\\/\\/[^\\s/$.?#].[^\\s]*$", ErrorMessage ="Write correct url")]
    public string? SuccessUrl {get; set;}
    
    [RegularExpression("^(https?|ftp):\\/\\/[^\\s/$.?#].[^\\s]*$", ErrorMessage = "Write correct url")]
    public string? CancelUrl  {get; set;}

}
