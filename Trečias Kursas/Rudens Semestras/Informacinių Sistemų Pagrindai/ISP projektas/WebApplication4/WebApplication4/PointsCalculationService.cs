using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication4.Repositories;

public class PointsCalculationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public PointsCalculationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Initial delay before starting the loop
        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Run your periodic logic here
                using (var scope = _serviceProvider.CreateScope())
                {
                    var likesRepo = scope.ServiceProvider.GetRequiredService<LikesRepo>();

                    // Example: Calculate and add points
                    likesRepo.CalculateAndAddLikesAsPoints();
                }

                // Sleep for a week (adjust the duration as needed)
                await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
            }
            catch (Exception ex)
            {
                // Log any errors
                Console.WriteLine($"Error in PointsCalculationService: {ex.Message}");
            }
        }
    }
}