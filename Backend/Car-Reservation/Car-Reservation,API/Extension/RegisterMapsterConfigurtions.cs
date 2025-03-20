﻿using Car_Reservation.Dtos.CarDtos;
using Car_Reservation.Dtos.ReservationDtos;
using Car_Reservation.Dtos.ReviewDtos;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.CarEntity;
using Mapster;
using System.Reflection;
using System.Runtime.Serialization;

namespace Car_Reservation_API.Extension;

internal static class RegisterMapsterConfigurtions
{

    public static IServiceCollection RegisterMapsterConfigurtion(this IServiceCollection service)
    {

        //config.NewConfig<Reservation, ReservationToReturnDto>()
        //      .Map(dest => dest.FormattedDate, src => src.ReservationDate.ToString("yyyy-MM-dd HH:mm"));
        var config = TypeAdapterConfig.GlobalSettings;

        // Define Mapping Configuration
        config.NewConfig<Reservation, ReservationToReturnDto>().TwoWays();
        config.NewConfig<Review, ReviewToReturnDto>()
            .Map(d=>d.UserEmail,s=>s.User.Email)  ;

        config.NewConfig<Car, CarToReturnDto>()
            .Map(d => d.Brand, s => s.Brand.name)
            .Map(d => d.Model, s => s.Model.Name);

        config.NewConfig<Brand, BrandToReturnDto>()
            .Map(d => d.Name, s => s.name);

        config.NewConfig<Model, ModelToReturnDto>()
            .Map(d => d.Category, s => s.category.ToString());




        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        return service;
    }


    
}

