using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Application.Common.Models;

namespace AudioDelivery.Application.Search.DTOs;

/// <summary>
/// Combined search results returned by GET /api/v1/search.
/// Each property is a paginated list of items for that content type.
///
/// The client specifies which types to search via the "type" query parameter
/// (e.g., type=album,track). Only the requested types will be populated;
/// others will remain null.
///
/// See: https://developer.spotify.com/documentation/web-api/reference/search
/// </summary>
public class SearchResultDto
{
    public PaginatedResult<AlbumSummaryDto>? Albums { get; set; }
    public PaginatedResult<ArtistDto>? Artists { get; set; }
    public PaginatedResult<TrackDto>? Tracks { get; set; }
    public PaginatedResult<PlaylistSummaryDto>? Playlists { get; set; }
}
