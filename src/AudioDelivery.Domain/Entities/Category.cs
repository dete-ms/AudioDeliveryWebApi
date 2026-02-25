using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a browse category (e.g., "Top Lists", "Mood", "Workout").
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// The name of the category (e.g., "Top Lists").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    // ── Navigation Properties ──

    /// <summary>
    /// Category icon images (one-to-many).
    /// </summary>
    public ICollection<Image> Images { get; set; } = new List<Image>();

    /// <summary>
    /// Playlists belonging to this category (many-to-many).
    /// </summary>
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}
