using Car_Reservation.Repository.Contexts.CarRentContext.Data;
using Microsoft.EntityFrameworkCore;

namespace Car_Reservation_API.Extension
{
    internal static class AppDbContextsExtension
    {

        public static IServiceCollection AddAppDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarRentContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            return services;
        }
    }
}