using Car_Reservation_Domain.ServicesInterfaces;
using Car_Reservation.Services;
using Car_Reservation.Services.EmailService;
using Car_Reservation_Domain.Entities.EmailEntity;
using Car_Reservation.APIs.Controllers;
using Car_Reservation.DTOs.AccountDtos;
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
        services.AddScoped<IReservationService, ReservationService>();
        services.AddSwaggerService();
        services.AddTransient<ISendEmail, EmailServices>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddTransient<IPaymentService,StripePaymentService>();
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
        services.AddHostedService<ReservationCleanupService>();


        return services;
    }
}
