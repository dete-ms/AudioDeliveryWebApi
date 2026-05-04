using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Domain.Events;

/// <summary>
/// Raised when a user is deleted.
/// Carries image data from the user's playlists (which will be SQL-cascade-deleted
/// and therefore won't raise their own domain events).
public class UserDeletedEvent : IDomainEvent
{
    private readonly EntityType _entityType = EntityType.User;

    public UserDeletedEvent(IReadOnlyList<string> playlistImageUrls, IReadOnlyList<Guid> playlistImageIds)
    {
        this.PlaylistImageUrls = playlistImageUrls;
        this.PlaylistImageIds = playlistImageIds;
    }

    /// <summary>
    /// Gets the Image blob URLs from all playlists owned by the deleted user.
    /// </summary>
    public IReadOnlyList<string> PlaylistImageUrls { get; }

    /// <summary>
    /// Gets the Image entity IDs from all playlists owned by the deleted user,
    /// for orphan detection.
    /// </summary>
    public IReadOnlyList<Guid> PlaylistImageIds { get; }

    public Guid EntityId { get; set; }

    /// <summary>
    /// Gets or sets the type of entity associated with the event. Only the value <see cref="EntityType.User"> is supported.
    /// </summary>
    /// <remarks>Setting this property to a value other than <see cref="EntityType.User"> will result in an exception. This
    /// property enforces that the event is always related to a user entity.</remarks>
    public EntityType EntityType
    {
        get
        {
            return _entityType;
        }
        set
        {
            if (value != EntityType.User)
            {
                throw new InvalidOperationException($"EntityType for {nameof(UserDeletedEvent)} must be {EntityType.User}.");
            }
        }
    }
}
