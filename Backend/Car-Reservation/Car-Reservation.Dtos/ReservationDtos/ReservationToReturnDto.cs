using Car_Reservation_Domain.Entities.CarEntity;
using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Dtos.ReservationDtos
{
    public class ReservationToReturnDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CarId { get; set; }  
    }
}
