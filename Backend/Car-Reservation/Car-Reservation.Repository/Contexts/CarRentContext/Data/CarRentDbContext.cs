using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.CarEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Car_Reservation.Repository.Contexts.CarRentContext.Data;

public class CarRentDbContext(DbContextOptions options):IdentityDbContext<User>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public DbSet<Car> cars { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Request> Requests { get; set; }
}
