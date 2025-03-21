using Car_Reservation_Domain.Entities.CarEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Repository.Contexts.CarRentContext.Configrations
{
    class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Model> builder)
        {
            builder.Property(m=>m.Category).HasConversion(
                category => category.ToString(),
                category => (Category)Enum.Parse(typeof(Category), category)
                );

            builder.Property(m => m.Name).HasMaxLength(60);


            builder.HasData(
                // Toyota Models
                new Model { Id = 1, Name = "Corolla", Category = Category.Economic, BrandId = 1 },
                new Model { Id = 2, Name = "Prius", Category = Category.SUV, BrandId = 1 },
                new Model { Id = 3, Name = "Camry", Category = Category.Luxury, BrandId = 1 },
                new Model { Id = 4, Name = "Land Cruiser", Category = Category.Pickup, BrandId = 1 },
                new Model { Id = 5, Name = "RAV4", Category = Category.Economic, BrandId = 1 },

                // Ford Models
                new Model { Id = 6, Name = "Mustang", Category = Category.Sports, BrandId = 2 },
                new Model { Id = 7, Name = "F-150", Category = Category.Pickup, BrandId = 2 },
                new Model { Id = 8, Name = "Focus", Category = Category.Economic, BrandId = 2 },

                // BMW Models
                new Model { Id = 9, Name = "3 Series", Category = Category.Luxury, BrandId = 3 },
                new Model { Id = 10, Name = "X5", Category = Category.SUV, BrandId = 3 },

                // Mercedes-Benz Models
                new Model { Id = 11, Name = "C-Class", Category = Category.Luxury, BrandId = 4 },
                new Model { Id = 12, Name = "GLE", Category = Category.SUV, BrandId = 4 },

                // Honda Models
                new Model { Id = 13, Name = "Civic", Category = Category.Economic, BrandId = 5 },
                new Model { Id = 14, Name = "CR-V", Category = Category.SUV, BrandId = 5 },

                // Chevrolet Models
                new Model { Id = 15, Name = "Camaro", Category = Category.Sports, BrandId = 6 },
                new Model { Id = 16, Name = "Silverado", Category = Category.Pickup, BrandId = 6 },

                // Nissan Models
                new Model { Id = 17, Name = "Altima", Category = Category.Medium, BrandId = 7 },
                new Model { Id = 18, Name = "Rogue", Category = Category.SUV, BrandId = 7 },

                // Kia Models
                new Model { Id = 19, Name = "Forte", Category = Category.Economic, BrandId = 9 },
                new Model { Id = 20, Name = "Sorento", Category = Category.SUV, BrandId = 9 },

                // Volkswagen Models
                new Model { Id = 21, Name = "Golf", Category = Category.Hatchback, BrandId = 10 },
                new Model { Id = 22, Name = "Tiguan", Category = Category.SUV, BrandId = 10 },

                // Mazda Models
                new Model { Id = 23, Name = "Mazda3", Category = Category.Economic, BrandId = 12 },
                new Model { Id = 24, Name = "CX-5", Category = Category.SUV, BrandId = 12 },

                // Dodge Models
                new Model { Id = 25, Name = "Charger", Category = Category.Sports, BrandId = 14 },
                new Model { Id = 26, Name = "Durango", Category = Category.SUV, BrandId = 14 },

                // Jeep Models
                new Model { Id = 27, Name = "Wrangler", Category = Category.SUV, BrandId = 15 },
                new Model { Id = 28, Name = "Grand Cherokee", Category = Category.SUV, BrandId = 15 },

                // Tesla Models
                new Model { Id = 29, Name = "Model 3", Category = Category.Electric, BrandId = 16 },
                new Model { Id = 30, Name = "Model Y", Category = Category.Electric, BrandId = 16 },

                // Volvo Models
                new Model { Id = 31, Name = "XC60", Category = Category.SUV, BrandId = 17 },
                new Model { Id = 32, Name = "S60", Category = Category.Luxury, BrandId = 17 },

                // Porsche Models
                new Model { Id = 33, Name = "911", Category = Category.Sports, BrandId = 18 },
                new Model { Id = 34, Name = "Cayenne", Category = Category.SUV, BrandId = 18 }
            );
        }
    }
}
