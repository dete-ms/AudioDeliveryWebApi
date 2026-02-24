using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Copyright statement attached to an Album.
/// </summary>
public class Copyright : BaseEntity
{
    /// <summary>
    /// The copyright text for this content.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// The type of copyright: "C" = copyright, "P" = sound recording (performance) copyright.
    /// Stored as a single character string.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    // ── Relationship ──

    /// <summary>
    /// FK to the Album this copyright belongs to.
    /// </summary>
    public Guid AlbumId { get; set; }
    public Album Album { get; set; } = null!;
}
