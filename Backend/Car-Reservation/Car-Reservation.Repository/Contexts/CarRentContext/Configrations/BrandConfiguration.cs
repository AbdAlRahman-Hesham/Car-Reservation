using Car_Reservation_Domain.Entities.CarEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Repository.Contexts.CarRentContext.Configrations
{
    class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(b => b.name).HasMaxLength(60);
            builder.HasData(
            new Brand { Id = 1, name = "Toyota" },
            new Brand { Id = 2, name = "Ford" },
            new Brand { Id = 3, name = "BMW" },
            new Brand { Id = 4, name = "Mercedes-Benz" },
            new Brand { Id = 5, name = "Honda" },
            new Brand { Id = 6, name = "Chevrolet" },
            new Brand { Id = 7, name = "Nissan" },
            new Brand { Id = 8, name = "Hyundai" },
            new Brand { Id = 9, name = "Kia" },
            new Brand { Id = 10, name = "Volkswagen" },
            new Brand { Id = 11, name = "Subaru" },
            new Brand { Id = 12, name = "Mazda" },
            new Brand { Id = 13, name = "Lexus" },
            new Brand { Id = 14, name = "Dodge" },
            new Brand { Id = 15, name = "Jeep" },
            new Brand { Id = 16, name = "Tesla" },
            new Brand { Id = 17, name = "Volvo" },
            new Brand { Id = 18, name = "Porsche" }
        );
        }
    }
}
