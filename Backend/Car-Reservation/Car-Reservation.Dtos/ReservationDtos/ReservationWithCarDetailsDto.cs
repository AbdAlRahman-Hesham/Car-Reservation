﻿using Car_Reservation.Dtos.CarDtos;
using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Dtos.ReservationDtos
{
    public class ReservationWithCarDetailsDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CarId { get; set; }
        public string UserEmail { get; set; }
        
        // Car details
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
        public decimal CarPrice { get; set; }
        public string CarImageUrl { get; set; }
    }
}
