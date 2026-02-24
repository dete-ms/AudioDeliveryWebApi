namespace AudioDelivery.Application.Tracks.DTOs;

/// <summary>
/// Full track details returned by GET /api/v1/tracks/{id}.
/// Maps to Spotify's TrackObject.
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-track
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
    public string? Isrc { get; set; }

    // TODO: Add Album (AlbumSummaryDto) and Artists (List<ArtistSummaryDto>)
}
