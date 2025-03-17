using Car_Reservation.Dtos.ReservationDtos;
using Car_Reservation_Domain.Entities;
using Mapster;
using System.Reflection;

namespace Car_Reservation_API.Extension;

internal static class RegisterMapsterConfigurtions
{

    public static IServiceCollection RegisterMapsterConfigurtion(this IServiceCollection service)
    {

        //config.NewConfig<Reservation, ReservationToReturnDto>()
        //      .Map(dest => dest.FormattedDate, src => src.ReservationDate.ToString("yyyy-MM-dd HH:mm"));
        var config = TypeAdapterConfig.GlobalSettings;

        // Define Mapping Configuration
        config.NewConfig<Reservation, ReservationToReturnDto>().TwoWays(); ;





        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        return service;
    }
}

