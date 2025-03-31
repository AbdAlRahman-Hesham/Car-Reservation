using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities;
using System.Linq.Expressions;

public class StaleReservationSpecification : Specification<Reservation>
{
    public StaleReservationSpecification(DateTime expiryTime)
        : base(DefaultIncludes,r => (r.Status == ReservationStatus.Pending || r.Status == ReservationStatus.PaymentPending)&& r.ReservationDate <= expiryTime)
    {
    }
    private static readonly List<Expression<Func<Reservation, object>>> DefaultIncludes = new() { r => r.User, r => r.Car };

}