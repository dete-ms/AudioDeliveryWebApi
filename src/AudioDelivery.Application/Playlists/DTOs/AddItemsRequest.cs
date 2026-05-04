namespace AudioDelivery.Application.Playlists.DTOs;

/// <summary>
/// Request body for POST /api/v1/playlists/{id}/items (add items to a playlist).
/// </summary>
public class AddItemsRequest
{
    /// <summary>
    /// List of item IDs to add.
    /// </summary>
    public IList<Guid> Ids { get; set; } = new List<Guid>();

    /// <summary>
    /// The position to insert the items (zero-based).
    /// If omitted, items are appended to the end.
    /// </summary>
    public int? Position { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether duplicate entries are allowed.
    /// </summary>
    public bool AllowDuplicates { get; set; } = false;
}
