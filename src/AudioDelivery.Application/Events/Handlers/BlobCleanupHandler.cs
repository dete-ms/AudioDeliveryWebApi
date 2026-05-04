using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Events;
using Microsoft.Extensions.Logging;
using MediatR;

namespace AudioDelivery.Application.Events.Handlers;

public class BlobCleanupHandler : INotificationHandler<EntityDeletedEvent>
{
    private readonly IStorageService _storageService;
    private readonly ILogger<BlobCleanupHandler> _logger;

    public BlobCleanupHandler(
        IStorageService storageService,
        ILogger<BlobCleanupHandler> logger)
    {
        _storageService = storageService;
        _logger = logger;
    }

    public async Task Handle(EntityDeletedEvent notification, CancellationToken cancellationToken)
    {
        foreach (var imageUrl in notification.ImageUrls)
        {
            try
            {
                await _storageService.DeleteAsync(imageUrl);
                _logger.LogInformation("Deleted blob at URL: {ImageUrl}", imageUrl);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to delete blob for image URL: {ImageUrl}. " +
                                       "It will be cleaned up by the scheduled orphan job.", imageUrl);
            }
        }
    }
}
