﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Dtos.DashBoard
{
    public class CarUtilizationDto
    {
        // Overall metrics
        public int TotalCars { get; set; }
        public int CurrentlyRentedCars { get; set; }
        public int AvailableCars { get; set; }
        public decimal OverallUtilizationRate { get; set; } // Percentage of time cars are rented
        
        // Top performing cars
        public List<CarUtilizationDetailDto> MostUtilizedCars { get; set; } = new List<CarUtilizationDetailDto>();
        public List<CarUtilizationDetailDto> LeastUtilizedCars { get; set; } = new List<CarUtilizationDetailDto>();
        
        // Utilization by brand and model
        public List<BrandUtilizationDto> UtilizationByBrand { get; set; } = new List<BrandUtilizationDto>();
        
        // Idle time analysis
        public decimal AverageIdleDays { get; set; } // Average days a car sits idle between reservations
    }

    public class CarUtilizationDetailDto
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public decimal UtilizationRate { get; set; } // Percentage of time the car is rented
        public int ReservationCount { get; set; }
        public decimal Revenue { get; set; }
        public bool IsCurrentlyRented { get; set; }
    }

    public class BrandUtilizationDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int TotalCars { get; set; }
        public decimal AverageUtilizationRate { get; set; }
        public List<ModelUtilizationDto> Models { get; set; } = new List<ModelUtilizationDto>();
    }

    public class ModelUtilizationDto
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int CarCount { get; set; }
        public decimal UtilizationRate { get; set; }
    }
}
