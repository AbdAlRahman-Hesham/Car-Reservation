using Car_Reservation.Repository.UnitOfWork;

namespace Car_Reservation_API.Extension
{
    internal static class AppUnitOfWorkExtensionHelpers
    {


        public static IServiceCollection AddUnitsOfWork(this IServiceCollection service)
        {

            service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            return service;
        }
    }
}