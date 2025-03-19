using Car_Reservation_Domain.Entities.CarEntity;
using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Car_Reservation_Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public string? PaymentIntentId { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CarId { get; set; }
        public Car  car{ get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
