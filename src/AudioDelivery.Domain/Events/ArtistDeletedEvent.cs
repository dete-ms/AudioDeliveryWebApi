using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Domain.Events;

/// <summary>
/// Raised when an artist is deleted.
/// Carries the IDs of albums that were associated with the artist
/// so handlers can check for orphaned albums (albums with zero remaining artists).
/// </summary>
public class ArtistDeletedEvent : IDomainEvent
{
    private EntityType _entityType = EntityType.Artist;

    public ArtistDeletedEvent(IReadOnlyList<Guid> albumIds)
    {
        AlbumIds = albumIds;
    }

    /// <summary>
    /// Gets the collection of unique identifiers for the associated albums.
    /// </summary>
    public IReadOnlyList<Guid> AlbumIds { get; }

    public Guid EntityId { get; set; }

    /// <summary>
    /// Gets or sets the type of entity associated with the event. Only the value <see cref="EntityType.Artist"> is supported.
    /// </summary>
    /// <remarks>Setting this property to a value other than <see cref="EntityType.Artist"> will result in an exception. This
    /// property enforces that the event is always related to an artist entity.</remarks>
    public EntityType EntityType 
    {
        get 
        {
            return _entityType;
        }
        set
        {
            if (value != EntityType.Artist)
            {
                throw new InvalidOperationException($"EntityType for {nameof(ArtistDeletedEvent)} must be {EntityType.Artist}.");
            }
        }
    }
}
