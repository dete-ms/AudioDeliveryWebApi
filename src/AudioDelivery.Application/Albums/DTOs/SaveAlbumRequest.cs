namespace AudioDelivery.Application.Albums.DTOs;

/// <summary>
/// Request body for saving/liking albums (PUT /api/v1/me/albums).
/// Spotify accepts an array of album IDs in the request body.
/// </summary>
public class SaveAlbumRequest
{
    /// <summary>
    /// List of album IDs to save to the user's library.
    /// </summary>
    public IList<Guid> Ids { get; set; } = new List<Guid>();
}
