namespace AudioDelivery.Application.Common.Models;

/// <summary>
/// Generic wrapper for paginated API responses.
///
/// This mirrors Spotify's pagination pattern where every list endpoint returns:
///   { href, items, limit, offset, next, previous, total }
///
/// Usage:
///   return new PaginatedResult&lt;AlbumDto&gt;
///   {
///       Items = albumDtos,
///       Total = totalCount,
///       Limit = limit,
///       Offset = offset,
///       Href = $"/api/v1/albums?offset={offset}&amp;limit={limit}"
///   };
///
/// The Next/Previous properties can be computed from Offset, Limit, and Total.
///
/// See: Any Spotify endpoint returning a "paging object"
/// </summary>
/// <typeparam name="T">The type of items in the page.</typeparam>
public class PaginatedResult<T>
{
    /// <summary>
    /// A link to the Web API endpoint returning the full result of the request.
    /// </summary>
    public string Href { get; set; } = string.Empty;

    /// <summary>
    /// The items in this page of results.
    /// </summary>
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();

    /// <summary>
    /// The maximum number of items in the response (as set in the query or by default).
    /// </summary>
    public int Limit { get; set; }

    /// <summary>
    /// The offset of the items returned (as set in the query or by default).
    /// </summary>
    public int Offset { get; set; }

    /// <summary>
    /// URL to the next page of items (null if none).
    /// </summary>
    public string? Next { get; set; }

    /// <summary>
    /// URL to the previous page of items (null if none).
    /// </summary>
    public string? Previous { get; set; }

    /// <summary>
    /// The total number of items available to return.
    /// </summary>
    public int Total { get; set; }
}
