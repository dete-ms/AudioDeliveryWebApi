using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Events;
using Microsoft.Extensions.Logging;
using MediatR;

namespace AudioDelivery.Application.Events.Handlers;

public class LibraryItemCleanupEventHandler : INotificationHandler<EntityDeletedEvent>
{
    private readonly IUserLibraryRepository _libraryRepository;
    private readonly IMediator _medaiator;
    private readonly ILogger<LibraryItemCleanupEventHandler> _logger;

    public LibraryItemCleanupEventHandler(
        IUserLibraryRepository libraryRepository,
        IMediator mediator,
        ILogger<LibraryItemCleanupEventHandler> logger)
    {
        _libraryRepository = libraryRepository;
        _medaiator = mediator;
        _logger = logger;
    }

    public Task Handle(EntityDeletedEvent notification, CancellationToken cancellationToken)
    {
        return _libraryRepository.DeleteItemsRelatedToEntityAsync(notification.EntityId, notification.EntityType, cancellationToken);
    }
}
