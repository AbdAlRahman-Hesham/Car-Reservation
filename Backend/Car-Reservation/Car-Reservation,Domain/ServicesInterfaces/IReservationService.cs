using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation_Domain.ServicesInterfaces
{
    public interface IReservationService
    {
        public  Task<IReadOnlyList<Reservation>?> GetAllReservationsForUser(string email);
        public Task<IReadOnlyList<Reservation>?> GetAllReservationsForUserByDate(string userEmail, DateTime date);
        public  Task<Reservation?> GetReservationById(int id);
        public Task<IReadOnlyList<Reservation>?> GetAllReservationsForCar(int carId);
        public  Task<Reservation?> GetReservationForCarByDate(int carId, DateTime date);
        public Task<IReadOnlyList<Reservation>?> GetCarReservationsByDates(int carId, DateTime startDate, DateTime endDate);

        public Task<Reservation?> MakeReservationForUser(string userId, DateTime StartDate, DateTime EndDate, int CarId);
        public  Task<Reservation?> CancleReservation(Reservation reslut);

    }
}
