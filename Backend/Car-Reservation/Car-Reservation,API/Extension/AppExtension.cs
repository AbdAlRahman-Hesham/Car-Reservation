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
        return services;
    }
}
