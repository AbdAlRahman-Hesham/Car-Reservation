﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Dtos.DashBoard
{
    public class ReservationAnalyticsDto
    {
        // Reservation counts
        public int TotalReservations { get; set; }
        public int PendingReservations { get; set; }
        public int ConfirmedReservations { get; set; }
        public int CancelledReservations { get; set; }
        public int PaymentPendingReservations { get; set; }
        public int PaymentFailedReservations { get; set; }

        // Reservation durations
        public decimal AverageReservationDuration { get; set; }

        // Time-based metrics
        public int ReservationsToday { get; set; }
        public int ReservationsThisWeek { get; set; }
        public int ReservationsThisMonth { get; set; }

        // Popular periods
        public List<DayOfWeekCount> PopularDaysOfWeek { get; set; } = new List<DayOfWeekCount>();
        public List<MonthCount> PopularMonths { get; set; } = new List<MonthCount>();

        // Conversion metrics
        public decimal ReservationConversionRate { get; set; } // Percentage of pending to confirmed
    }

    public class DayOfWeekCount
    {
        public string DayOfWeek { get; set; }
        public int Count { get; set; }
    }

    public class MonthCount
    {
        public string Month { get; set; }
        public int Count { get; set; }
    }
}
