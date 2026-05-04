using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

public class OrphanBlobCleanupJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OrphanBlobCleanupJob> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromHours(72);

    public OrphanBlobCleanupJob(
        IServiceScopeFactory scopeFactory,
        ILogger<OrphanBlobCleanupJob> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();
                var storageService = scope.ServiceProvider
                    .GetRequiredService<IStorageService>();

                // Find Image rows that have no associations in any join table
                var orphanedImages = await context.Images
                    .Where(i =>
                        !context.PlaylistImages.Any(pi => pi.ImageId == i.Id) &&
                        !context.CategoryImages.Any(ci => ci.ImageId == i.Id) &&
                        !context.ArtistImages.Any(ai => ai.ImageId == i.Id) &&
                        !context.AlbumImages.Any(ai => ai.ImageId == i.Id) &&
                        !context.UserImages.Any(ui => ui.ImageId == i.Id))
                    .ToListAsync(stoppingToken);

                _logger.LogInformation(
                    "Orphan cleanup found {Count} orphaned images", orphanedImages.Count);

                foreach (var image in orphanedImages)
                {
                    try
                    {
                        await storageService.DeleteAsync(image.Url);
                        context.Images.Remove(image);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex,
                            "Failed to delete orphan blob: {Url}", image.Url);
                    }
                }

                await context.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Orphan cleanup job failed");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}