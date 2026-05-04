using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Domain.Events;

/// <summary>
/// Raised when an entity that owns images is deleted.
/// Carries the image URLs so handlers can clean up blob storage.
/// </summary>
public class EntityDeletedEvent : IDomainEvent
{
    public EntityDeletedEvent(
        IReadOnlyList<Guid> imageIds, 
        IReadOnlyList<string> imageUrls)
    {
        this.ImageIds = imageIds;
        this.ImageUrls = imageUrls;
    }

    /// <summary>
    /// Gets the Image entity IDs, used for orphan detection (checking if any other entity still references the image).
    /// </summary>
    public IReadOnlyList<Guid> ImageIds { get; }

    /// <summary>
    /// Gets the blob URLs of images associated with the deleted entity.
    /// </summary>
    public IReadOnlyList<string> ImageUrls { get; }

    public Guid EntityId { get; set; }

    /// <summary>
    /// Gets the type of entity represented by this instance.
    /// </summary>
    public EntityType EntityType { get; set; }
}