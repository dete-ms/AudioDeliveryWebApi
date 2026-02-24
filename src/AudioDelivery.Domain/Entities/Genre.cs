using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a musical genre (e.g., "Rock", "Pop", "Hip-Hop").
/// </summary>
public class Genre : BaseEntity
{
    /// <summary>
    /// The genre name (e.g., "progressive-rock", "grunge").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    // ── Navigation Properties ──

    /// <summary>
    /// Artists associated with this genre (many-to-many).
    /// </summary>
    public ICollection<Artist> Artists { get; set; } = new List<Artist>();
}
