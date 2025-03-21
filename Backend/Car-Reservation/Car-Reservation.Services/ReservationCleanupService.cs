using Car_Reservation_Domain.ServicesInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class ReservationCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ReservationCleanupService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();
                await reservationService.AutoCancelStaleReservations();
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Run every hour
        }
    }
}
