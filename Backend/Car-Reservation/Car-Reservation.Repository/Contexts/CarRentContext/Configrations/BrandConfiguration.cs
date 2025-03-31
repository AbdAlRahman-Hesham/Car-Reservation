using Car_Reservation_Domain.Entities.CarEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car_Reservation.Repository.Contexts.CarRentContext.Configrations
{
    class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(60);

            builder.HasMany(b=>b.Models).WithOne(m=>m.Brand).HasForeignKey(m=>m.BrandId).OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Brand { Id = 1, Name = "Toyota", LogoUrl = "https://www.freepnglogos.com/uploads/toyota-logo-png/toyota-logos-brands-10.png" },
                new Brand { Id = 2, Name = "Ford", LogoUrl = "https://example.com/ford-logo.png" },
                new Brand { Id = 3, Name = "BMW", LogoUrl = "https://clipground.com/images/bmw-logo-png-5.png" },
                new Brand { Id = 4, Name = "Mercedes-Benz", LogoUrl = "https://example.com/mercedes-logo.png" },
                new Brand { Id = 5, Name = "Honda", LogoUrl = "https://example.com/honda-logo.png" },
                new Brand { Id = 6, Name = "Chevrolet", LogoUrl = "https://example.com/chevrolet-logo.png" },
                new Brand { Id = 7, Name = "Nissan", LogoUrl = "https://example.com/nissan-logo.png" },
                new Brand { Id = 9, Name = "Kia", LogoUrl = "https://example.com/kia-logo.png" },
                new Brand { Id = 10, Name = "Volkswagen", LogoUrl = "https://example.com/vw-logo.png" },
                new Brand { Id = 12, Name = "Mazda", LogoUrl = "https://example.com/mazda-logo.png" },
                new Brand { Id = 14, Name = "Dodge", LogoUrl = "https://example.com/dodge-logo.png" },
                new Brand { Id = 15, Name = "Jeep", LogoUrl = "https://example.com/jeep-logo.png" },
                new Brand { Id = 16, Name = "Tesla", LogoUrl = "https://example.com/tesla-logo.png" },
                new Brand { Id = 17, Name = "Volvo", LogoUrl = "https://example.com/volvo-logo.png" },
                new Brand { Id = 18, Name = "Porsche", LogoUrl = "https://example.com/porsche-logo.png" }
            );
        }
    }
}
