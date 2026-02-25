using AudioDelivery.Application.Search.DTOs;

namespace AudioDelivery.Application.Search;

/// <summary>
/// Service interface for the Search endpoint.
/// </summary>
public interface ISearchService
{
    /// <summary>
    /// GET /search?q=...&amp;type=...
    /// Search for albums, artists, tracks, and/or playlists matching a keyword.
    /// </summary>
    /// <param name="query">The search query string.</param>
    /// <param name="types">Comma-separated types to search: "album", "artist", "track", "playlist".</param>
    /// <param name="limit">Max results per type (default 20, max 50).</param>
    /// <param name="offset">Offset for pagination (default 0).</param>
    Task<SearchResultDto> SearchAsync(string query, string types, int limit = 20, int offset = 0, CancellationToken cancellationToken = default);
}
