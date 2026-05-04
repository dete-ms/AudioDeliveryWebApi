using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Tracks.DTOs;

namespace AudioDelivery.Application.Search.DTOs;

/// <summary>
/// Combined search results returned by GET /api/v1/search.
/// Each property is a paginated list of items for that content type.
///
/// The client specifies which types to search via the "type" query parameter
/// (e.g., type=album,track). Only the requested types will be populated;
/// others will remain null.
/// </summary>
public class SearchResultDto
{
    public PaginatedResult<AlbumSummaryDto>? Albums { get; set; }
    public PaginatedResult<ArtistSummaryDto>? Artists { get; set; }
    public PaginatedResult<TrackDto>? Tracks { get; set; }
    public PaginatedResult<PlaylistSummaryDto>? Playlists { get; set; }

    /// <summary>
    /// Copies all non-null and non-empty properties from the source <see cref="SearchResultDto"/> to the target <see
    /// cref="SearchResultDto"/> instance.
    /// </summary>
    /// <remarks>Only the Albums, Artists, Tracks, and Playlists properties are considered. Properties in
    /// <paramref name="target"/> are updated only if the corresponding property in <paramref name="source"/> is neither
    /// null nor empty. Other properties remain unchanged.</remarks>
    /// <param name="source">The <see cref="SearchResultDto"/> instance from which non-null, non-empty properties are copied.</param>
    /// <param name="target">The <see cref="SearchResultDto"/> instance to which non-null, non-empty properties are assigned. Properties in this
    /// instance are overwritten only if the corresponding property in <paramref name="source"/> is neither null nor empty.</param>
    /// <returns>The <paramref name="target"/> instance with updated properties from <paramref name="source"/>.</returns>
    public static SearchResultDto CopyFilled(SearchResultDto source, SearchResultDto target)
    {
        if (source.Albums != null && source.Albums.Items.Any()) target.Albums = source.Albums;
        if (source.Artists != null && source.Artists.Items.Any()) target.Artists = source.Artists;
        if (source.Tracks != null && source.Tracks.Items.Any()) target.Tracks = source.Tracks;
        if (source.Playlists != null && source.Playlists.Items.Any()) target.Playlists = source.Playlists;
        return target;
    }
}
