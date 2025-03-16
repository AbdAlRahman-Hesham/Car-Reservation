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
    class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasMany(c => c.Reservations).WithOne(r => r.car);
            builder.HasMany(c => c.Reviews).WithOne(r => r.Car);
            builder.HasOne(c => c.Admin).WithMany();

            builder.Property(c=>c.Url).HasMaxLength(500);
         


        }
    }
}
