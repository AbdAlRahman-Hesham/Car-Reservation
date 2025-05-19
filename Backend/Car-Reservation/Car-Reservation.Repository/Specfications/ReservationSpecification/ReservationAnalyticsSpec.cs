﻿using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Repository.Specfications.ReservationSpecification
{
    public class ReservationAnalyticsSpec : Specification<Reservation>
    {
        // For getting all reservations with status filter
        public ReservationAnalyticsSpec(ReservationStatus? status = null) 
            : base(DefaultIncludes, status.HasValue ? r => r.Status == status.Value : null)
        {
        }

        // For getting reservations within a time period
        public ReservationAnalyticsSpec(DateTime startDate, DateTime endDate) 
            : base(DefaultIncludes, r => r.StartDate >= startDate && r.StartDate <= endDate)
        {
        }

        // For getting reservations by status within a time period
        public ReservationAnalyticsSpec(ReservationStatus status, DateTime startDate, DateTime endDate) 
            : base(DefaultIncludes, r => r.Status == status && r.StartDate >= startDate && r.StartDate <= endDate)
        {
        }

        // Default includes for reservation analytics
        private static readonly List<Expression<Func<Reservation, object>>> DefaultIncludes = new() 
        { 
            r => r.User, 
            r => r.Car,
            r => r.Car.Model,
            r => r.Car.Brand
        };
    }
}
