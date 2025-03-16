﻿using Car_Reservation.Repository.Contexts.CarRentContext.Data;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.CarEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ECommerce.Repository.Data
{
    public static class Seeding
    {
        public static void SeedingHelper(CarRentDbContext context)
        {
            string filePath = "D:\\NET\\Projects\\Car_Revesation\\Backend\\Car-Reservation\\Car-Reservation.Repository\\Contexts\\CarRentContext\\DataSeeding";
            seed<Brand>(filePath + "/brand.json", context);
            seed<Model>(filePath + "/model.json", context);
            //seed<Car>(filePath + "/cars.json", context);
            //seed<Review>(filePath + "/reviews.json", context);
            //seed<Reservation>(filePath + "/reservations.json", context);
            //seed<Request>(filePath + "/requests.json", context);
        }

        public static void seed<T>(string path, CarRentDbContext context) where T : BaseEntity
        {
            var file = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<List<T>>(file);
            if (context.Set<T>().Count() == 0) // Fix the Set method call
            {
                if (data?.Count > 0)
                {
                    foreach (var item in data)
                    {
                        context.Set<T>().Add(item);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
