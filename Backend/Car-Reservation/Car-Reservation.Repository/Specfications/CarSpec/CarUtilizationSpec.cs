﻿using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.CarEntity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Repository.Specfications.CarSpec
{
    public class CarUtilizationSpec : Specification<Car>
    {
        // For getting all cars with their reservations
        public CarUtilizationSpec() 
            : base(DefaultIncludes)
        {
        }

        // For getting cars with reservations in a specific time period
        public CarUtilizationSpec(DateTime startDate, DateTime endDate) 
            : base(DefaultIncludes, c => c.Reservations.Any(r => 
                (r.StartDate >= startDate && r.StartDate <= endDate) || 
                (r.EndDate >= startDate && r.EndDate <= endDate) ||
                (r.StartDate <= startDate && r.EndDate >= endDate)))
        {
        }

        // For getting cars by utilization status (rented or available)
        public CarUtilizationSpec(bool isRented) 
            : base(DefaultIncludes, c => isRented ? 
                c.Reservations.Any(r => r.Status != ReservationStatus.Cancelled && 
                    r.StartDate <= DateTime.Now && r.EndDate >= DateTime.Now) : 
                !c.Reservations.Any(r => r.Status != ReservationStatus.Cancelled && 
                    r.StartDate <= DateTime.Now && r.EndDate >= DateTime.Now))
        {
        }

        // For getting cars by brand with their utilization data
        public CarUtilizationSpec(int brandId) 
            : base(DefaultIncludes, c => c.BrandId == brandId)
        {
        }

        // For getting cars by model with their utilization data
        public CarUtilizationSpec(int? brandId, int modelId) 
            : base(DefaultIncludes, c => (brandId == null || c.BrandId == brandId) && c.ModelId == modelId)
        {
        }

        // Default includes for car utilization
        private static readonly List<Expression<Func<Car, object>>> DefaultIncludes = new() 
        { 
            c => c.Reservations,
            c => c.Model,
            c => c.Brand
        };
    }
}
