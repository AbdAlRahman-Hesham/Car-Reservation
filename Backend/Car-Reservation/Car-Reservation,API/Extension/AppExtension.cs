using Car_Reservation_Domain.ServicesInterfaces;
using Car_Reservation.Services;
namespace Car_Reservation_API.Extension;

public static class AppExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAppDbContexts(configuration);
        services.AddUnitsOfWork();
        services.AddIdentityAndJwtAuthenticationServices(configuration);
        services.AddApiErrorServices();
        services.RegisterMapsterConfigurtion();
        services.AddScoped<IReservationService, ReservartionService>();
        return services;
    }
}
