using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a market (country) where content is available.
/// </summary>
public class Market : BaseEntity
{
    /// <summary>
    /// ISO 3166-1 alpha-2 country code (e.g., "US", "GB").
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Human-readable country name (e.g., "United States").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    // ── Navigation Properties ──

    /// <summary>
    /// Albums available in this market (many-to-many).
    /// </summary>
    public ICollection<Album> Albums { get; set; } = new List<Album>();

    /// <summary>
    /// Tracks available in this market (many-to-many).
    /// </summary>
    public ICollection<Track> Tracks { get; set; } = new List<Track>();
}
