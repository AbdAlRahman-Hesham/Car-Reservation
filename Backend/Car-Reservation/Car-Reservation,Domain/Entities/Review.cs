using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.Entities.CarEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation_Domain.Entities
{
    public class Review : BaseEntity
    {
        public int Stars { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }

}
