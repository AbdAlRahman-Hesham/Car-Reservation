using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.AccountDtos;

public class UserAddressDto
{
    [Required]
    public string Street { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
}