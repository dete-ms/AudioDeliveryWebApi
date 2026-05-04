using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Events;
using MediatR;

namespace AudioDelivery.Application.Events.Handlers;

public class OrphanImageCleanupHandler : INotificationHandler<EntityDeletedEvent>
{
    private readonly IImageRepository _imageRepository;

    public OrphanImageCleanupHandler(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task Handle(
        EntityDeletedEvent notification,
        CancellationToken cancellationToken)
    {
        foreach (var imageId in notification.ImageIds)
        {
            var image = await _imageRepository.GetByIdTrackedAsync(imageId);

            if (image == null) continue;

            if (await _imageRepository.IsOrphanedAsync(imageId))
                _imageRepository.Delete(image);
        }

        await _imageRepository.SaveChangesAsync();
    }
}
