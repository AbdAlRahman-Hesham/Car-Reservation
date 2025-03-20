using Car_Reservation_Domain.Entities.CarEntity;
using Car_Reservation_Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Dtos.ReviewDtos
{
   public  class ReviewDto
    {
        [Required]
        [Range(1,5)]
        public int Stars { get; set; }
        [Required]
        [StringLength(500)]
        public string Comment { get; set; }
        [Required]
        public int CarId { get; set; }

    }
}
