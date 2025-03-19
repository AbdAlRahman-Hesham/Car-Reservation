using System.ComponentModel.DataAnnotations;

namespace Car_Reservation.Dtos.CarDtos;

public class CarDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public bool IsAvailable { get; set; }

    [Required]
    [Url]
    public string Url { get; set; }

    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public double Rating { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Insurance cost must be a positive number.")]
    public decimal InsuranceCost { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public decimal Price { get; set; }

    public string? AdminId { get; set; } // Nullable if Admin is optional

    [Required]
    public int ModelId { get; set; }

    [Required]
    public int BrandId { get; set; }
}