using Car_Reservation_Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car_Reservation.Repository.Contexts.CarRentContext.Configrations;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(u=>u.Address, a =>
        {
            a.WithOwner();
        });
    }
}
