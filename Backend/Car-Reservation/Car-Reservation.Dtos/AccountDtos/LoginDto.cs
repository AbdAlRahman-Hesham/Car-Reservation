using System.ComponentModel.DataAnnotations;

namespace Car_Reservation.DTOs.AccountDtos;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
