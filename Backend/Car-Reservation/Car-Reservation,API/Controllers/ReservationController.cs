using Car_Reservation.APIs.Controllers;
using Car_Reservation_Domain.Entities.CarEntity;
using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Car_Reservation_Domain.ServicesInterfaces;
using Car_Reservation.DTOs.ErrorResponse;
using Mapster;
using Car_Reservation.Dtos.ReservationDtos;
using Microsoft.AspNetCore.Identity;

namespace Car_Reservation_API.Controllers
{
   /// <summary>
   /// Reservations Controller 

//{ 

//Get All Reservations For Use

//Get All Reservations For Use By Date

//Get Reservations By Id

//Get All Reservations For Car

//Get Reservation For Car By Date

//Cancel Reservations By Id --> Admin, User Who Make It

//Make Reservation For Use

//}
/// </summary>
public class ReservationController : BaseApiController
    {
        private readonly IReservationService _reservationService;
        private readonly UserManager<User> _userManager;

        public ReservationController(IReservationService reservationService , UserManager<User> userManager)
        {
            this._reservationService = reservationService;
            this._userManager = userManager;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ReservationToReturnDto>>> GetAllReservationsForUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var reslut = await _reservationService.GetAllReservationsForUser(userEmail);
            if (reslut == null) { return NotFound(new ApiResponse(404)); } 
            var reslutDto = reslut.Adapt<IReadOnlyList<ReservationToReturnDto>>();
            return Ok(reslutDto);

        }

        //Get All Reservations For Use By Date
        [Authorize]
        [HttpGet("date")]
        public async Task<ActionResult<IReadOnlyList<ReservationToReturnDto>>> GetAllReservationsForUserByDate(DateTime date)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var reslut = await _reservationService.GetAllReservationsForUserByDate(userEmail,date);
            if (reslut == null) { return NotFound(new ApiResponse(404)); }

            var reslutDto = reslut.Adapt<IReadOnlyList<ReservationToReturnDto>>();
            return Ok(reslutDto);
        }
        //Get Reservations By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationToReturnDto>> GetReservationsById(int id)
        {
            var reslut = await _reservationService.GetReservationById(id);
            if (reslut == null) { return NotFound(new ApiResponse(404)); }
            var reslutDto = reslut.Adapt<ReservationToReturnDto>();
            return Ok(reslutDto);
        }
        //Get All Reservations For Car

        [HttpGet("car/{carId}")]
        public async Task<ActionResult<IReadOnlyList<ReservationToReturnDto>>> GetAllReservationsForCar(int carId)
        {
            var reslut = await _reservationService.GetAllReservationsForCar(carId);
            if (reslut == null) { return NotFound(new ApiResponse(404)); }
            var reslutDto = reslut.Adapt<IReadOnlyList<ReservationToReturnDto>>();
            return Ok(reslutDto);
        }
        //Get Reservation For Car By Date
        [HttpGet("car")]
        public async Task<ActionResult<ReservationToReturnDto>> GetAllReservationsForCarByDate(int carId, DateTime date)
        {
            var reslut = await _reservationService.GetReservationForCarByDate(carId, date);
            if (reslut == null) { return NotFound(new ApiResponse(404)); }
            var reslutDto = reslut.Adapt<ReservationToReturnDto>();
            return Ok(reslutDto);
        }

        //Make Reservation For Use
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReservationToReturnDto>> MakeReservationForUser(int CarId, DateTime StartDate, DateTime EndDate)
        {

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail!);

            var reslut = await _reservationService.MakeReservationForUser(user!.Id,StartDate,EndDate,CarId);
            if (reslut == null) { return BadRequest(new ApiResponse(400)); }
            var reslutDto = reslut.Adapt<ReservationToReturnDto>();
            return Ok(reslutDto);
        }
        //get car reservaton with start and end date
        [HttpGet("car/ByDates")]
        public async Task<ActionResult<IReadOnlyList<ReservationToReturnDto>>> GetCarReservationsByDates(int carId, DateTime startDate, DateTime endDate)
        {
            var reslut = await _reservationService.GetCarReservationsByDates(carId, startDate, endDate);
            if (reslut == null) { return NotFound(new ApiResponse(404)); }
            var reslutDto = reslut.Adapt<IReadOnlyList<ReservationToReturnDto>>();
            return Ok(reslutDto);
        }

        //Cancel Reservations By Id --> Admin, User Who Make It
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ReservationToReturnDto>> CancleReservation(int id)
        {

            var reslut = await _reservationService.GetReservationById(id);
            if (reslut == null) { return NotFound(new ApiResponse(404));  }

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail!);
            if (!(reslut.UserId == user!.Id || User.IsInRole("Admin")) ) { return BadRequest(new ApiResponse(401)); }

            var CanceldReservation =  await _reservationService.CancleReservation(reslut);
            var reslutDto = CanceldReservation.Adapt<ReservationToReturnDto>();
            return reslutDto;
        }
    }
}
