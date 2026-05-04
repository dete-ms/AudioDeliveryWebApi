namespace AudioDelivery.Application.Playlists.DTOs;

/// <summary>
/// Request body for POST /api/v1/playlists/{id}/items (add items to a playlist).
/// </summary>
public class RemoveItemsRequest
{
    /// <summary>
    /// List of item IDs to add.
    /// </summary>
    public IList<Guid> Ids { get; set; } = new List<Guid>();
}
