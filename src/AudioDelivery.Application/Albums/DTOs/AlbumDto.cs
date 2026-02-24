namespace AudioDelivery.Application.Albums.DTOs;

/// <summary>
/// Full album details returned by GET /api/v1/albums/{id}.
/// Maps to Spotify's AlbumObject.
///
/// TODO: Add properties matching the Spotify response:
///   - Id, Name, AlbumType, TotalTracks, ReleaseDate, ReleaseDatePrecision
///   - Popularity, Label, Uri, ExternalUrl
///   - Artists (list of ArtistSummaryDto)
///   - Images (list of ImageDto)
///   - Copyrights, AvailableMarkets
///   - Tracks (PaginatedResult of TrackDto — optional, may come from a sub-endpoint)
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-an-album
/// </summary>
public class AlbumDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AlbumType { get; set; } = string.Empty;
    public int TotalTracks { get; set; }
    public string ReleaseDate { get; set; } = string.Empty;
    public string ReleaseDatePrecision { get; set; } = string.Empty;
    public int Popularity { get; set; }
    public string? Label { get; set; }
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }

    // TODO: Add nested DTOs for Artists, Images, Copyrights, AvailableMarkets
}
