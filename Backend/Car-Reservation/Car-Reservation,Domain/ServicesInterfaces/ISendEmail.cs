using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.EmailEntity;
using Car_Reservation_Domain.Entities.Identity;

namespace Car_Reservation_Domain.ServicesInterfaces;

public interface ISendEmail
{
    public Task SendAsync(Email email);
    public Task SendConfirmationEmailAsync(Reservation reservation, User user);
    public Task SendReservationCanceledEmailAsync(Reservation reservation, string modelName);
}
