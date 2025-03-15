using Mapster;
using System.Reflection;

namespace Car_Reservation_API.Extension
{
    internal static class RegisterMapsterConfigurtions
    {

        public static IServiceCollection RegisterMapsterConfigurtion(this IServiceCollection service)
        {

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            return service;
        }
    }
}