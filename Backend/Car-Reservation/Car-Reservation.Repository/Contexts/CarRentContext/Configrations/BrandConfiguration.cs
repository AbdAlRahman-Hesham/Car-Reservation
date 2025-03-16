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
        }
    }
}
