using Car_Reservation.Repository.Contexts.CarRentContext.Data;
using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation.APIs.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Car_Reservation.Repository.Identity.DataSeeding;



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
            await userManager.SeedingUserAsync();
        }
        return app;
    }

   /* public static IApplicationBuilder StartRedisServer(this IApplicationBuilder app)
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c redis-server",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };
            Process process = new Process { StartInfo = psi };
            process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            process.ErrorDataReceived += (sender, args) => Console.WriteLine("Error: " + args.Data);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            // Store the process in a static variable or DI container
            // so it can be properly disposed when the application shuts down
            AppDomain.CurrentDomain.ProcessExit += (s, e) => {
                try
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                    process.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error shutting down Redis: " + ex.Message);
                }
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return app;
    }*/
}
