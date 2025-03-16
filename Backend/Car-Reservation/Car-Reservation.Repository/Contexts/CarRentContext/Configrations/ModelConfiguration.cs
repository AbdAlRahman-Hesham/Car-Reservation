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

        }
    }
}
