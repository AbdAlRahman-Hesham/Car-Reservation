using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Car_Reservation.Repository.Specfications.ReservationSpecification
{
    public class ReservationSpecWithCarId : Specification<Reservation>
    {
        public ReservationSpecWithCarId(int carId) : base(r => r.CarId == carId)
        {
        }

        public ReservationSpecWithCarId(int carId, DateTime startDate, DateTime endDate) : base(r =>
            r.CarId == carId &&
            ((r.StartDate >= startDate && r.StartDate <= endDate) ||
             (r.EndDate <= endDate && r.EndDate >= startDate)) &&
            r.Status != ReservationStatus.Cancelled)
        {
        }
    }
}