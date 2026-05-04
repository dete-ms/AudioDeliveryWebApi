using AudioDelivery.Domain.Events;
using Microsoft.Extensions.Logging;
using MediatR;

namespace AudioDelivery.Application.Events.Handlers;

public class CascadedPlaylistCleanupHandler : INotificationHandler<UserDeletedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<CascadedPlaylistCleanupHandler> _logger;

    public CascadedPlaylistCleanupHandler(
        IMediator mediator, 
        ILogger<CascadedPlaylistCleanupHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        if (notification.PlaylistImageIds.Count == 0)
            return;

        _logger.LogInformation("User deletion cascade: publishing EntityDeletedEvent for {Count} " +
                               "playlist images.", notification.PlaylistImageUrls.Count);

        await _mediator.Publish(new EntityDeletedEvent(
            notification.PlaylistImageIds,
            notification.PlaylistImageUrls
        ), cancellationToken);
    }
}
