using Car_Reservation_Domain.Entities.CarEntity;
using Car_Reservation_Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Dtos.ReviewDtos
{
   public  class ReviewToReturnDto
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
        public int CarId { get; set; }
        public string UserEmail { get; set; }

    }
}
