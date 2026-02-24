namespace AudioDelivery.Application.Playlists.DTOs;

/// <summary>
/// Request body for POST /api/v1/playlists/{id}/tracks (add items to a playlist).
///
/// See: https://developer.spotify.com/documentation/web-api/reference/add-tracks-to-playlist
/// </summary>
public class AddItemsRequest
{
    /// <summary>
    /// List of track URIs or IDs to add.
    /// </summary>
    public IList<string> Uris { get; set; } = new List<string>();

    /// <summary>
    /// The position to insert the items (zero-based).
    /// If omitted, items are appended to the end.
    /// </summary>
    public int? Position { get; set; }
}
