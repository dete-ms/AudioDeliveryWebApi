using AudioDelivery.Application.Library.DTOs;

namespace AudioDelivery.Application.Library;

/// <summary>
/// Unified Library service – mirrors Spotify's /me/library endpoints.
///
/// Endpoints covered:
///   PUT    /api/v1/me/library          → Save items to library
///   DELETE /api/v1/me/library          → Remove items from library
///   GET    /api/v1/me/library/contains → Check if items are saved
/// </summary>
public interface ILibraryService
{
    /// <summary>
    /// PUT /me/library – Save one or more items to the current user's library.
    /// </summary>
    /// <param name="userId">The current user's ID.</param>
    /// <param name="request">Request containing comma-separated Spotify URIs (max 40).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SaveItemsAsync(Guid userId, LibraryItemRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /me/library – Remove one or more items from the current user's library.
    /// </summary>
    /// <param name="userId">The current user's ID.</param>
    /// <param name="request">Request containing comma-separated Spotify URIs (max 40).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RemoveItemsAsync(Guid userId, LibraryItemRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /me/library/contains – Check if one or more items are saved in the user's library.
    /// </summary>
    /// <param name="userId">The current user's ID.</param>
    /// <param name="uris">Comma-separated Spotify URIs to check (max 40).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ordered bool array: true if the corresponding URI is saved, false otherwise.</returns>
    Task<LibraryCheckResult> CheckItemsAsync(Guid userId, string uris, CancellationToken cancellationToken = default);
}
