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
    class ReservataionConfiguraton : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(r => r.User).WithMany(u=>u.Reservations).HasForeignKey(r=>r.UserId);
            builder.HasOne(r=>r.car).WithMany(c => c.Reservations).HasForeignKey(r => r.CarId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(r=>r.Status).HasConversion(
                status => status.ToString(),
                status => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), status)
                );
            
        }
    }
}
