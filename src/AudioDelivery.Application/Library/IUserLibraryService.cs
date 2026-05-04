using AudioDelivery.Application.Common.DTOs;
using AudioDelivery.Application.Library.DTOs;

namespace AudioDelivery.Application.Library;

/// <summary>
/// Unified Library service – mirrors Spotify's /me/library endpoints.
/// </summary>
public interface IUserLibraryService
{
    /// <summary>
    /// PUT /me/library – Save one or more items to the current user's library.
    /// </summary>
    /// <param name="userId">The current user's ID.</param>
    /// <param name="request">Request containing comma-separated Spotify URIs (max 40).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    Task SaveItemsAsync(Guid userId, LibraryItemRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /me/library – Remove one or more items from the current user's library.
    /// </summary>
    /// <param name="userId">The current user's ID.</param>
    /// <param name="request">Request containing comma-separated Spotify URIs (max 40).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    Task RemoveItemsAsync(Guid userId, LibraryItemRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /me/library/contains - Asynchronously determines whether the specified items are saved by the given user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose saved items are to be checked.</param>
    /// <param name="request">A <see cref="LibraryItemRequest"/> containing the unique identifiers of the items to check for in the user's saved items.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ItemCheckResult"/> with a 
    /// list of boolean values indicating whether each item is saved by the user.</returns>
    Task<ItemCheckResult> CheckItemsAsync(Guid userId, LibraryItemRequest request, CancellationToken cancellationToken = default);
}
