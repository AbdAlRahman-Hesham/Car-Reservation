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
            builder.Property(m=>m.category).HasConversion(
                category => category.ToString(),
                category => (Category)Enum.Parse(typeof(Category), category)
                );

            builder.Property(m => m.Name).HasMaxLength(60);

            builder.HasData(
                new Model { Id = 1, Name = "Corolla", category = Category.Economic, BrandId = 1 },
                new Model { Id = 2, Name = "Camry", category = Category.Medium, BrandId = 1 },
                new Model { Id = 3, Name = "Mustang", category = Category.Economic, BrandId = 2 },
                new Model { Id = 4, Name = "Explorer", category = Category.Luxury, BrandId = 2 },
                new Model { Id = 5, Name = "1 Series", category = Category.Economic, BrandId = 3 },
                new Model { Id = 6, Name = "3 Series", category = Category.Medium, BrandId = 3 },
                new Model { Id = 7, Name = "X5", category = Category.Luxury, BrandId = 3 },
                new Model { Id = 8, Name = "C-Class", category = Category.Medium, BrandId = 4 },
                new Model { Id = 9, Name = "GLC", category = Category.Medium, BrandId = 4 },
                new Model { Id = 10, Name = "Civic", category = Category.Economic, BrandId = 5 },
                new Model { Id = 11, Name = "Accord", category = Category.Medium, BrandId = 5 },
                new Model { Id = 12, Name = "Corvette", category = Category.Medium, BrandId = 6 },
                new Model { Id = 13, Name = "Camaro", category = Category.Medium, BrandId = 6 },
                new Model { Id = 14, Name = "Altima", category = Category.Medium, BrandId = 7 },
                new Model { Id = 15, Name = "Maxima", category = Category.Medium, BrandId = 7 },
                new Model { Id = 16, Name = "Sonata", category = Category.Medium, BrandId = 8 },
                new Model { Id = 17, Name = "Tucson", category = Category.Medium, BrandId = 8 },
                new Model { Id = 18, Name = "Optima", category = Category.Medium, BrandId = 9 },
                new Model { Id = 19, Name = "Luxuryage", category = Category.Medium, BrandId = 9 },
                new Model { Id = 20, Name = "Golf", category = Category.Economic, BrandId = 10 },
                new Model { Id = 21, Name = "Tiguan", category = Category.Medium, BrandId = 10 },
                new Model { Id = 22, Name = "Outback", category = Category.Medium, BrandId = 11 },
                new Model { Id = 23, Name = "Forester", category = Category.Medium, BrandId = 11 },
                new Model { Id = 24, Name = "CX-5", category = Category.Medium, BrandId = 12 },
                new Model { Id = 25, Name = "MX-5 Miata", category = Category.Luxury, BrandId = 12 },
                new Model { Id = 26, Name = "ES", category = Category.Luxury, BrandId = 13 },
                new Model { Id = 27, Name = "RX", category = Category.Luxury, BrandId = 13 },
                new Model { Id = 28, Name = "Charger", category = Category.Luxury, BrandId = 14 },
                new Model { Id = 29, Name = "Challenger", category = Category.Luxury, BrandId = 14 },
                new Model { Id = 30, Name = "Wrangler", category = Category.Medium, BrandId = 15 },
                new Model { Id = 31, Name = "Grand Cherokee", category = Category.Medium, BrandId = 15 },
                new Model { Id = 32, Name = "Model 3", category = Category.Economic, BrandId = 16 },
                new Model { Id = 33, Name = "XC90", category = Category.Luxury, BrandId = 17 },
                new Model { Id = 34, Name = "911", category = Category.Luxury, BrandId = 18 }
            );
        }
    }
}
