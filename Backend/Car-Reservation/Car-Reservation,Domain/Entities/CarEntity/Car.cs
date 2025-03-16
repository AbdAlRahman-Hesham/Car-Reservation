using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Car_Reservation_Domain.Entities.CarEntity

{
    public class Car : BaseEntity
    {
        public Model Model { get; set; }
        public  Brand brand { get; set; }
        public bool IsAvailable { get; set; }
        public string Url { get; set; }
        public double Rating { get; set; }
        public decimal InsuranceCost { get; set; } 
        public decimal Price { get; set; }
        public string AdminId { get; set; }
        public User Admin { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
