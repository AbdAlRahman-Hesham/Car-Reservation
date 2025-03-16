using System.ComponentModel.DataAnnotations;

namespace Car_Reservation.DTOs.AccountDtos;

public class UserAddressDto
{
    [Required]
    public string Street { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
}