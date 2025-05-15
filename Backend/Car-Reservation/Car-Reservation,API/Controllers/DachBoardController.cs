using Car_Reservation.APIs.Controllers;
using Car_Reservation.Dtos.DashBoard;
using Car_Reservation.Repository.Specfications.ReservationSpecification;
using Car_Reservation.Repository.Specifications;
using Car_Reservation.Repository.UnitOfWork;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.CarEntity;
using Car_Reservation_Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_Reservation_API.Controllers
{

    public class DachBoardController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public DachBoardController(IUnitOfWork unitOfWork , UserManager<User> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }
        [HttpGet("FastStatistic")]
        public async Task<ActionResult<Statistic>> FastStatistic()
        {

            var totalCars = await _unitOfWork.Repository<Car>().GetCountAsync(c=> true);

            var availableCars = await _unitOfWork.Repository<Car>().GetCountAsync(c => c.IsAvailable);

            
            var rentedCars = await _unitOfWork.Repository<Car>().GetCountAsync(c => c.Reservations.Any(r=>r.Status != ReservationStatus.Cancelled));

            var totalCustomers = await _userManager.Users.CountAsync();

            var spec = new ReservaationInThisMonthSpec();

            var revs = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);

          var   monthlyRevenue = revs.ToList().Sum(r => (r.Car.Price * ((r.EndDate - r.StartDate).Days)+1) + r.Car.InsuranceCost);

            return Ok(new
            Statistic
            {
                TotalCars = totalCars,
                AvailableCars = availableCars,
                RentedCars = rentedCars,
                TotalCustomers = totalCustomers,
                MonthlyRevenue = monthlyRevenue
            });

        }

    }
}
