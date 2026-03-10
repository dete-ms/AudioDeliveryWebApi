using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Images.DTOs;
using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Application.Albums.DTOs;

/// <summary>
/// Full album details returned by GET /api/v1/albums/{id}.
/// </summary>
public class AlbumDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AlbumType AlbumType { get; set; }
    public bool IsPublic { get; set; }
    public int TotalTracks { get; set; }
    public string ReleaseDate { get; set; } = string.Empty;
    public ReleaseDatePrecision ReleaseDatePrecision { get; set; }
    public int Popularity { get; set; }
    public string? Label { get; set; }
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }
    public IList<ArtistDto> Artists { get; set; } = null!;
    public IList<TrackDto> Tracks { get; set; } = null!;
    public IList<ImageDto> Images { get; set; } = null!;
}
