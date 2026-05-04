using AudioDelivery.Domain.Enums;
using MediatR;

namespace AudioDelivery.Domain.Events;

/// <summary>
/// Marker interface for domain events.
/// Extends MediatR's INotification so events can be published via the mediator.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    Guid EntityId { get; set; }

    /// <summary>
    /// Gets or sets the type of entity represented by this instance.
    /// </summary>
    EntityType EntityType { get; set; }
}
