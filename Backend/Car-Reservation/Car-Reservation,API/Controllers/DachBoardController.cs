using Car_Reservation.APIs.Controllers;
using Car_Reservation.Dtos.DashBoard;
using Car_Reservation.Dtos.ReservationDtos;
using Car_Reservation.DTOs.ErrorResponse;
using Car_Reservation.Repository.Specfications.CarSpec;
using Car_Reservation.Repository.Specfications.ReservationSpecification;
using Car_Reservation.Repository.UnitOfWork;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.CarEntity;
using Car_Reservation_Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Car_Reservation_API.Controllers
{

    public class DachBoardController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public DachBoardController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        /*[HttpGet("FastStatistic")]
        public async Task<ActionResult<Statistic>> FastStatistic()
        {

            var totalCars = await _unitOfWork.Repository<Car>().GetCountAsync(c=> true);

            var availableCars = await _unitOfWork.Repository<Car>().GetCountAsync(c => c.IsAvailable);


            var rentedCars = await _unitOfWork.Repository<Car>().GetCountAsync(c => c.Reservations.Any(r=>r.Status != ReservationStatus.Cancelled));

            var totalCustomers = await _userManager.Users.CountAsync();

            var spec = new ReservaationInThisMonthSpec();

            var revs = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);

            var monthlyRevenue = revs.ToList().Sum(r => r.Car.Price * ((r.EndDate - r.StartDate).Days + 1) + r.Car.InsuranceCost);

            return Ok(new
            Statistic
            {
                TotalCars = totalCars,
                AvailableCars = availableCars,
                RentedCars = rentedCars,
                TotalCustomers = totalCustomers,
                MonthlyRevenue = monthlyRevenue
            });

        }*/

        [HttpGet("DetailedStatistics")]
        public async Task<ActionResult<Statistic>> DetailedStatistics()
        {
            // Basic statistics
            var totalCars = await _unitOfWork.Repository<Car>().GetCountAsync(c => true);
            var availableCars = await _unitOfWork.Repository<Car>().GetCountAsync(c => c.IsAvailable);
            var rentedCars = await _unitOfWork.Repository<Car>().GetCountAsync(c => c.Reservations.Any(r => r.Status != ReservationStatus.Cancelled));
            var totalCustomers = await _userManager.Users.CountAsync();

            // Get current month reservations
            var currentMonthSpec = new ReservaationInThisMonthSpec();
            var currentMonthReservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(currentMonthSpec);
            var monthlyRevenue = currentMonthReservations.ToList().Sum(r => r.Car.Price * ((r.EndDate - r.StartDate).Days + 1) + r.Car.InsuranceCost);

            // Get all active reservations (not cancelled)
            var activeReservationsSpec = new ReservationSpec(r => r.Status != ReservationStatus.Cancelled);
            var activeReservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(activeReservationsSpec);

            // Calculate total revenue from all active reservations
            var totalRevenue = activeReservations.ToList().Sum(r => r.Car.Price * ((r.EndDate - r.StartDate).Days + 1) + r.Car.InsuranceCost);

            // Get average reservation duration
            var avgDuration = activeReservations.Any()
                ? activeReservations.Average(r => (r.EndDate - r.StartDate).Days + 1)
                : 0;

            return Ok(new Statistic
            {
                TotalCars = totalCars,
                AvailableCars = availableCars,
                RentedCars = rentedCars,
                TotalCustomers = totalCustomers,
                MonthlyRevenue = monthlyRevenue,
                TotalRevenue = totalRevenue,
                AverageReservationDuration = (decimal)avgDuration
            });
        }

        [HttpGet("ReservationsByDateRange")]
        [ProducesResponseType(typeof(IReadOnlyList<ReservationWithCarDetailsDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult<IReadOnlyList<ReservationWithCarDetailsDto>>> GetReservationsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            // Validate date range
            if (startDate > endDate)
            {
                return BadRequest(new ApiResponse(400, "Start date must be before or equal to end date"));
            }

            // Create specification for reservations in the date range with includes
            var reservationsSpec = new ReservationSpec(r =>
                (r.StartDate >= startDate && r.StartDate <= endDate) || // Reservation starts within range
                (r.EndDate >= startDate && r.EndDate <= endDate) ||     // Reservation ends within range
                (r.StartDate <= startDate && r.EndDate >= endDate)      // Reservation spans the entire range
            );

            // Get reservations
            var reservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(reservationsSpec);

            // Map to DTOs with car details
            var reservationDtos = reservations.Select(r => new ReservationWithCarDetailsDto
            {
                Id = r.Id,
                Status = r.Status.ToString(),
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                CarId = r.CarId,
                UserEmail = r.User?.Email ?? "Unknown",
                CarModel = r.Car?.Model?.Name ?? "Unknown Model",
                CarBrand = r.Car?.Brand?.Name ?? "Unknown Brand",
                CarPrice = r.Car?.Price ?? 0,
                CarImageUrl = r.Car?.Url ?? ""
            }).ToList();

            return Ok(reservationDtos);
        }

        [HttpGet("ReservationAnalytics")]
        [ProducesResponseType(typeof(ReservationAnalyticsDto), 200)]
        public async Task<ActionResult<ReservationAnalyticsDto>> GetReservationAnalytics([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            // Set default date range if not provided (last 30 days)
            var end = endDate ?? DateTime.Now;
            var start = startDate ?? end.AddDays(-30);

            // Get all reservations
            var allReservationsSpec = new ReservationAnalyticsSpec();
            var allReservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(allReservationsSpec);

            // Get reservations in the specified date range
            var dateRangeSpec = new ReservationAnalyticsSpec(start, end);
            var reservationsInRange = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(dateRangeSpec);

            // Calculate reservation counts by status
            var pendingCount = reservationsInRange.Count(r => r.Status == ReservationStatus.Pending);
            var confirmedCount = reservationsInRange.Count(r => r.Status == ReservationStatus.Confirmed);
            var cancelledCount = reservationsInRange.Count(r => r.Status == ReservationStatus.Cancelled);
            var paymentPendingCount = reservationsInRange.Count(r => r.Status == ReservationStatus.PaymentPending);
            var paymentFailedCount = reservationsInRange.Count(r => r.Status == ReservationStatus.PaymentFailed);

            // Calculate average reservation duration
            var avgDuration = reservationsInRange.Any()
                ? reservationsInRange.Average(r => (r.EndDate - r.StartDate).Days + 1)
                : 0;

            // Calculate time-based metrics
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var reservationsToday = reservationsInRange.Count(r => r.StartDate.Date == today);
            var reservationsThisWeek = reservationsInRange.Count(r => r.StartDate >= startOfWeek && r.StartDate <= today);
            var reservationsThisMonth = reservationsInRange.Count(r => r.StartDate >= startOfMonth && r.StartDate <= today);

            // Calculate popular days of week
            var popularDays = reservationsInRange
                .GroupBy(r => r.StartDate.DayOfWeek)
                .Select(g => new DayOfWeekCount
                {
                    DayOfWeek = g.Key.ToString(),
                    Count = g.Count()
                })
                .OrderByDescending(d => d.Count)
                .ToList();

            // Calculate popular months
            var popularMonths = reservationsInRange
                .GroupBy(r => r.StartDate.Month)
                .Select(g => new MonthCount
                {
                    Month = new DateTime(2000, g.Key, 1).ToString("MMMM"),
                    Count = g.Count()
                })
                .OrderByDescending(m => m.Count)
                .ToList();

            // Calculate conversion rate (pending to confirmed)
            var totalPendingOrConfirmed = pendingCount + confirmedCount;
            var conversionRate = totalPendingOrConfirmed > 0
                ? (decimal)confirmedCount / totalPendingOrConfirmed * 100
                : 0;

            // Create and return the analytics DTO
            var analytics = new ReservationAnalyticsDto
            {
                TotalReservations = reservationsInRange.Count,
                PendingReservations = pendingCount,
                ConfirmedReservations = confirmedCount,
                CancelledReservations = cancelledCount,
                PaymentPendingReservations = paymentPendingCount,
                PaymentFailedReservations = paymentFailedCount,
                AverageReservationDuration = (decimal)avgDuration,
                ReservationsToday = reservationsToday,
                ReservationsThisWeek = reservationsThisWeek,
                ReservationsThisMonth = reservationsThisMonth,
                PopularDaysOfWeek = popularDays,
                PopularMonths = popularMonths,
                ReservationConversionRate = conversionRate
            };

            return Ok(analytics);
        }

        [HttpGet("CarUtilization")]
        [ProducesResponseType(typeof(CarUtilizationDto), 200)]
        public async Task<ActionResult<CarUtilizationDto>> GetCarUtilization([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            // Set default date range if not provided (last 30 days)
            var end = endDate ?? DateTime.Now;
            var start = startDate ?? end.AddDays(-30);
            var dateRange = (end - start).Days + 1;

            // Get all cars with their reservations
            var carUtilizationSpec = new CarUtilizationSpec();
            var allCars = await _unitOfWork.Repository<Car>().GetAllAsyncWithSpecification(carUtilizationSpec);

            // Get currently rented cars
            var currentlyRentedSpec = new CarUtilizationSpec(true);
            var currentlyRentedCars = await _unitOfWork.Repository<Car>().GetAllAsyncWithSpecification(currentlyRentedSpec);

            // Calculate overall metrics
            var totalCars = allCars.Count;
            var rentedCarsCount = currentlyRentedCars.Count;
            var availableCarsCount = totalCars - rentedCarsCount;

            // Calculate utilization for each car
            var carUtilizationDetails = new List<CarUtilizationDetailDto>();
            foreach (var car in allCars)
            {
                // Get active reservations in the date range
                var activeReservations = car.Reservations
                    .Where(r => r.Status != ReservationStatus.Cancelled &&
                        ((r.StartDate >= start && r.StartDate <= end) ||
                        (r.EndDate >= start && r.EndDate <= end) ||
                        (r.StartDate <= start && r.EndDate >= end)))
                    .ToList();

                // Calculate total days the car was rented in the date range
                int rentedDays = 0;
                foreach (var reservation in activeReservations)
                {
                    var reservationStart = reservation.StartDate < start ? start : reservation.StartDate;
                    var reservationEnd = reservation.EndDate > end ? end : reservation.EndDate;
                    rentedDays += (reservationEnd - reservationStart).Days + 1;
                }

                // Calculate utilization rate
                var utilizationRate = dateRange > 0 ? (decimal)rentedDays / dateRange * 100 : 0;

                // Calculate revenue
                var revenue = activeReservations.Sum(r =>
                    r.Car.Price * ((r.EndDate - r.StartDate).Days + 1) + r.Car.InsuranceCost);

                // Check if currently rented
                var isCurrentlyRented = car.Reservations.Any(r =>
                    r.Status != ReservationStatus.Cancelled &&
                    r.StartDate <= DateTime.Now &&
                    r.EndDate >= DateTime.Now);

                // Create car utilization detail
                carUtilizationDetails.Add(new CarUtilizationDetailDto
                {
                    CarId = car.Id,
                    Brand = car.Brand?.Name ?? "Unknown",
                    Model = car.Model?.Name ?? "Unknown",
                    ImageUrl = car.Url,
                    UtilizationRate = utilizationRate,
                    ReservationCount = activeReservations.Count,
                    Revenue = revenue,
                    IsCurrentlyRented = isCurrentlyRented
                });
            }

            // Get most and least utilized cars
            var mostUtilizedCars = carUtilizationDetails
                .OrderByDescending(c => c.UtilizationRate)
                .Take(5)
                .ToList();

            var leastUtilizedCars = carUtilizationDetails
                .OrderBy(c => c.UtilizationRate)
                .Take(5)
                .ToList();

            // Calculate utilization by brand
            var utilizationByBrand = carUtilizationDetails
                .GroupBy(c => new { BrandId = c.CarId, BrandName = c.Brand })
                .Select(g => new BrandUtilizationDto
                {
                    BrandId = g.Key.BrandId,
                    BrandName = g.Key.BrandName,
                    TotalCars = g.Count(),
                    AverageUtilizationRate = g.Average(c => c.UtilizationRate),
                    Models = g.GroupBy(c => new { ModelId = c.CarId, ModelName = c.Model })
                        .Select(mg => new ModelUtilizationDto
                        {
                            ModelId = mg.Key.ModelId,
                            ModelName = mg.Key.ModelName,
                            CarCount = mg.Count(),
                            UtilizationRate = mg.Average(c => c.UtilizationRate)
                        })
                        .ToList()
                })
                .ToList();

            // Calculate average idle days
            var totalIdleDays = 0;
            var idleCarCount = 0;
            foreach (var car in allCars)
            {
                var sortedReservations = car.Reservations
                    .Where(r => r.Status != ReservationStatus.Cancelled &&
                        ((r.StartDate >= start && r.StartDate <= end) ||
                        (r.EndDate >= start && r.EndDate <= end) ||
                        (r.StartDate <= start && r.EndDate >= end)))
                    .OrderBy(r => r.StartDate)
                    .ToList();

                if (sortedReservations.Count > 1)
                {
                    for (int i = 0; i < sortedReservations.Count - 1; i++)
                    {
                        var currentEnd = sortedReservations[i].EndDate;
                        var nextStart = sortedReservations[i + 1].StartDate;
                        if (nextStart > currentEnd)
                        {
                            totalIdleDays += (nextStart - currentEnd).Days - 1;
                        }
                    }
                    idleCarCount++;
                }
            }

            var averageIdleDays = idleCarCount > 0 ? (decimal)totalIdleDays / idleCarCount : 0;

            // Calculate overall utilization rate
            var overallUtilizationRate = totalCars > 0
                ? carUtilizationDetails.Average(c => c.UtilizationRate)
                : 0;

            // Create and return the car utilization DTO
            var carUtilization = new CarUtilizationDto
            {
                TotalCars = totalCars,
                CurrentlyRentedCars = rentedCarsCount,
                AvailableCars = availableCarsCount,
                OverallUtilizationRate = overallUtilizationRate,
                MostUtilizedCars = mostUtilizedCars,
                LeastUtilizedCars = leastUtilizedCars,
                UtilizationByBrand = utilizationByBrand,
                AverageIdleDays = averageIdleDays
            };

            return Ok(carUtilization);
        }
    }
}
