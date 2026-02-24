namespace AudioDelivery.Application.Artists.DTOs;

/// <summary>
/// Simplified artist representation used inside Album and Track responses.
/// Maps to Spotify's SimplifiedArtistObject.
/// </summary>
public class ArtistSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }
}
