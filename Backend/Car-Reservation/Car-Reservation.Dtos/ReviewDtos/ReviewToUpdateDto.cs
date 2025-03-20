using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Dtos.ReviewDtos
{
    public class ReviewToUpdateDto
    {
        [Required]
        public  int Id { get; set; }
        [Required]
        [Range(1, 5)]
        public int Stars { get; set; }
        [Required]
        [StringLength(500)]
        public string Comment { get; set; }
        
    }
}
