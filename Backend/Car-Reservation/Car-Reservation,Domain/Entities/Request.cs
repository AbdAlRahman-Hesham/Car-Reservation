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
    public class Request : BaseEntity
    {
        public DateTime RequestedAt { get; set; }
        public string RequestedModel { get; set; }
        public string RequestedBrand { get; set; }
        public string UserId { get; set; }
        public ICollection<User> User { get; set; }
    }
}
