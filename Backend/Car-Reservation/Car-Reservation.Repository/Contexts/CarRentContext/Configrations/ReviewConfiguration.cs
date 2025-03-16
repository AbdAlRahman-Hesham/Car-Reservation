using Car_Reservation_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Repository.Contexts.CarRentContext.Configrations
{
    class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(r=>r.User).WithMany();
            builder.Property(r => r.Comment).HasMaxLength(500);
            builder.HasOne(r => r.Car).WithMany(c => c.Reviews).HasForeignKey(r => r.CarId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
