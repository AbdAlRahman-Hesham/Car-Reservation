using Car_Reservation_Domain.ServicesInterfaces;
using Car_Reservation.Services;
using Car_Reservation.Services.EmailService;
using Car_Reservation_Domain.Entities.EmailEntity;
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
        services.AddSwaggerService();
        services.AddTransient<ISendEmail, EmailServices>();
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));


        return services;
    }
}
