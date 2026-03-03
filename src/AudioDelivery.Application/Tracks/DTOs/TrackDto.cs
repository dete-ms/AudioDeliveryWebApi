using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Artists.DTOs;

namespace AudioDelivery.Application.Tracks.DTOs;

/// <summary>
/// Full track details returned by GET /api/v1/tracks/{id}.
/// </summary>
public class TrackDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DiscNumber { get; set; }
    public int TrackNumber { get; set; }
    public int DurationMs { get; set; }
    public bool Explicit { get; set; }
    public int Popularity { get; set; }
    public string? PreviewUrl { get; set; }
    public bool IsLocal { get; set; }
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }
    public AlbumSummaryDto Album { get; set; } = null!;
    public IList<ArtistSummaryDto> Artists { get; set; } = null!;
}
