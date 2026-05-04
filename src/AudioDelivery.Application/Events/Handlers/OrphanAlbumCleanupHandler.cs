using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Events;
using Microsoft.Extensions.Logging;
using MediatR;

namespace AudioDelivery.Application.Events.Handlers;

public class OrphanAlbumCleanupHandler : INotificationHandler<ArtistDeletedEvent>
{
    private readonly IAlbumRepository _albumRepository;
    private readonly ILogger<OrphanAlbumCleanupHandler> _logger;

    public OrphanAlbumCleanupHandler(
        IAlbumRepository albumRepository,
        ILogger<OrphanAlbumCleanupHandler> logger)
    {
        _albumRepository = albumRepository;
        _logger = logger;
    }

    public async Task Handle(ArtistDeletedEvent notification, CancellationToken cancellationToken)
    {
        foreach (var albumId in notification.AlbumIds)
        {
            try
            {
                var hasArtists = await _albumRepository.HasAnyArtistsAsync(albumId, cancellationToken);

                if (!hasArtists)
                {
                    _logger.LogInformation(
                        "Album {AlbumId} has no remaining artists after artist deletion. " +
                        "Deleting orphaned album.", albumId);

                    await _albumRepository.DeleteAlbumAsync(albumId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to delete orphan album with ID {AlbumId}", albumId);
            }
        }
    }
}
