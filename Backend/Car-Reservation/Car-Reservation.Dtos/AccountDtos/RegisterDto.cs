using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Car_Reservation.DTOs.AccountDtos;

public class RegisterDto
{
    [Required]
    public string FName { get; set; }
    [Required]
    public string LName { get; set; }
    [Required]
    public IFormFile Picture { get; set; } 
    [Required]
    [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must be exactly 14 digits.")]
    public string NationalId { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]
    [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Phone number must be number with 11 degit ")]
    public string PhoneNumber { get; set; }
    [Required]
    [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*])[A-Za-z\\d!@#$%^&*]{7,}$", ErrorMessage = "Password must be at least 7 characters long, include at least one uppercase letter, one lowercase letter, one digit, and one special character (!@#$%^&*).")]
    public string Password { get; set; }
    public UserAddressDto Address { get; set; }
}
