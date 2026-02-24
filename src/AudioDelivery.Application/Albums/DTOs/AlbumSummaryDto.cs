namespace AudioDelivery.Application.Albums.DTOs;

/// <summary>
/// Simplified album representation used when albums appear inside other responses
/// (e.g., in a Track response, the album field uses this simplified form).
/// Maps to Spotify's SimplifiedAlbumObject.
/// </summary>
public class AlbumSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AlbumType { get; set; } = string.Empty;
    public int TotalTracks { get; set; }
    public string ReleaseDate { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }

    // TODO: Add simplified Images and Artists lists
}
