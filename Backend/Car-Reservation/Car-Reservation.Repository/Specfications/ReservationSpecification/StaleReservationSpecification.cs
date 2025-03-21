using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities;

public class StaleReservationSpecification : Specification<Reservation>
{
    public StaleReservationSpecification(DateTime expiryTime)
        : base(r => r.Status == ReservationStatus.Pending && r.ReservationDate <= expiryTime)
    {
    }
}