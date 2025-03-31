using Car_Reservation.Repository.UnitOfWork;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Reservation.Repository.Specfications.ReservationSpecification;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Car_Reservation_Domain.Entities.CarEntity;

namespace Car_Reservation.Services
{
   public  class ReservartionService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservartionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Reservation>?> GetAllReservationsForUser(string email)
        {
            var spec = new ReservationSpec(email);
            var reservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);
            return reservations;
        }
        //all reservation for user in specific date
        public async  Task<IReadOnlyList<Reservation>?> GetAllReservationsForUserByDate(string userEmail, DateTime date)
        {
            var spec = new ReservationSpec(userEmail, date);
            var userReservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);

            return userReservations;
        }

        public async Task<Reservation?> GetReservationById(int id)
        {
            var spec = new ReservationSpec(id);
            var reservation = await _unitOfWork.Repository<Reservation>().GetAsyncWithSpecification(spec);
            return reservation;
        }
        public async Task<IReadOnlyList<Reservation>?> GetAllReservationsForCar(int carId)
        {
            var spec = new ReservationSpecWithCarId(carId);
            var reservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);
            return reservations;
        }
        public async Task<Reservation?> GetReservationForCarByDate(int carId,DateTime date)
        {
            var spec = new ReservationSpec(carId, date);
            //find the reservation that match owr date
            var reslut = await _unitOfWork.Repository<Reservation>().GetAsyncWithSpecification(spec);
            return reslut;

        }

        public async Task<IReadOnlyList<Reservation>?> GetCarReservationsByDates(int carId, DateTime startDate, DateTime endDate)
        {
            var spec = new ReservationSpec(carId, startDate, endDate);
            var carReservatoins = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);
            return carReservatoins;

        }

        public async Task<Reservation?> MakeReservationForUser(string userId, DateTime StartDate , DateTime EndDate,int CarId  )
        {
            //check if we have that car in database
            var car = await _unitOfWork.Repository<Car>().GetAsync(CarId);
            if (car == null) { return null; } // Car not found
            // Check if the car is available
            var spec = new ReservationSpecWithCarId(CarId, StartDate, EndDate);
            var carResertionInReqiuredDate = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);
          
            //TODO check if there is a free date between
            if (carResertionInReqiuredDate.Count>0) { return null; } // Car is not available

            // Create the reservation
            var reservation = new Reservation
            {
                CarId = CarId,
                StartDate = StartDate,
                EndDate = EndDate,
                UserId = userId,
                Status = ReservationStatus.Pending,
                
            };
            // TODO : Send Email to the user
            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.CompleteAsync();
            return reservation;
        }

        public async Task<Reservation?> CancleReservation(Reservation reslut)
        {
            reslut.Status = ReservationStatus.Cancelled;
            _unitOfWork.Repository<Reservation>().Update(reslut);
            await _unitOfWork.CompleteAsync();
            return reslut;
        }
        public async Task<bool> IsCarExist(int carId)
        {
            var car = await _unitOfWork.Repository<Car>().GetAsync(carId);
            return car != null;
        }
    }
}
