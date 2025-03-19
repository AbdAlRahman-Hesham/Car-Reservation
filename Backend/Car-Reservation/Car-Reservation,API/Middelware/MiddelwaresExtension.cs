using Car_Reservation.Repository.Contexts.CarRentContext.Data;
using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation.APIs.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Car_Reservation.Repository.Identity.DataSeeding;
using ECommerce.Repository.Data;



namespace Car_Reservation.APIs.Extensions;

public static class MiddelwaresExtension
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
    public static async Task<IApplicationBuilder> UseUpdateDataBase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<CarRentDbContext>();
            await db.Database.MigrateAsync(); 
            
            

        }
        return app;
    }
    public static async Task<IApplicationBuilder> UseSeeding(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await userManager.SeedingUserAsync(roleManager);

        }
        return app;
    }

}
