using Car_Reservation_Domain.Entities.CarEntity;
using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.Entities;

namespace Car_Reservation.Dtos.CarDtos;

public class CarToReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
    public string Url { get; set; }
    public double Rating { get; set; }
    public decimal InsuranceCost { get; set; }
    public decimal Price { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    
}
