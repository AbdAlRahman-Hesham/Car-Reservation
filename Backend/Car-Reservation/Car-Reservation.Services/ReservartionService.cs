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
        public async  Task<IReadOnlyList<Reservation>?> GetAllReservationsForUserByDate(string userEmail, DateTime date)
        {
            var userReservations = await GetAllReservationsForUser(userEmail);

            // Filter reservations based on the date range
            var filteredReservations = userReservations
                .Where(reservation => reservation.StartDate <= date && reservation.EndDate >= date)
                .ToList()
                .AsReadOnly();

            return filteredReservations;
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
            //get all reservations for car by date
            var carReservations = await GetAllReservationsForCar(carId);
            //find the reservation that match owr date
            var reslut = carReservations.Where(c=>c.StartDate<=date && c.EndDate>=date).SingleOrDefault();
            return reslut;

        }

        public async Task<IReadOnlyList<Reservation>?> GetCarReservationsByDates(int carId, DateTime startDate, DateTime endDate)
        {
            var carReservatoins = await GetAllReservationsForCar(carId);
            var filteredReservations = carReservatoins
                .Where(reservation => (reservation.StartDate >= startDate && reservation.StartDate <= endDate) ||(reservation.EndDate <= endDate && reservation.EndDate>=startDate))
                .ToList()
                .AsReadOnly();
            return filteredReservations;

        }

        public async Task<Reservation?> MakeReservationForUser(string userId, DateTime StartDate , DateTime EndDate,int CarId  )
        {
            //check if we have that car in database
            var car = await _unitOfWork.Repository<Car>().GetAsync(CarId);
            if (car == null) { return null; } // Car not found
            // Check if the car is available
            var carReservations = await GetAllReservationsForCar(CarId);
            var isCarAvailable = carReservations.All(reservation =>
                reservation.StartDate > EndDate ||
                reservation.EndDate < StartDate || 
                reservation.Status == ReservationStatus.Cancelled
            );
            if (!isCarAvailable) { return null; } // Car is not available

            // Create the reservation
            var reservation = new Reservation
            {
                CarId = CarId,
                StartDate = StartDate,
                EndDate = EndDate,
                UserId = userId,
                Status = ReservationStatus.Pending,
                
            };
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

        public async Task AutoCancelStaleReservations()
        {
            var now = DateTime.UtcNow;
            var staleReservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(
                new StaleReservationSpecification(now.AddHours(-24)) // Find pending reservations older than 24 hours
            );

            if (staleReservations.Any())
            {
                foreach (var reservation in staleReservations)
                {
                    reservation.Status = ReservationStatus.Cancelled;
                    _unitOfWork.Repository<Reservation>().Update(reservation);
                }

                await _unitOfWork.CompleteAsync();
            }
        }

    }
}
