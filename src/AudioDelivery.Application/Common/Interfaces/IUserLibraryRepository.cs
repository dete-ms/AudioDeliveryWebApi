using AudioDelivery.Application.Common.DTOs;
using AudioDelivery.Domain.JoinTables;
using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Application.Common.Interfaces;

public interface IUserLibraryRepository : IRepository<UserLibraryItem>
{
    /// <summary>
    /// Asynchronously adds a new item to the user's library.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to whom the item will be added.</param>
    /// <param name="entityId">The unique identifier of the entity to add to the user's library.</param>
    /// <param name="entityType">The type of the entity being added to the library.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    Task AddItemAsync(Guid userId, Guid entityId, EntityType entityType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the specified item for the given user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user who owns the item to be deleted.</param>
    /// <param name="itemId">The unique identifier of the item to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the item was
    /// successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteItemAsync(Guid userId, Guid itemId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes all items attached to the specified entity id and type asynchronously.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity for which all items will be deleted.</param>
    /// <param name="entitytype">The type of entity for which all items will be deleted.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if any items were
    /// deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteItemsRelatedToEntityAsync(Guid entityId, EntityType entitytype, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if the specified item is saved in the user's library.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="itemIds">The unique identifiers of the items to check.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns><see langword="true"/> if the item is saved in the user's library; otherwise, <see langword="false"/> .</returns>
    Task<ItemCheckResult> AreItemsSavedAsync(Guid userId, IList<Guid> itemIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the specified items from the user's library.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose library items are to be deleted.</param>
    /// <param name="itemIds">A list of unique identifiers representing the items to delete. Cannot be null or empty.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous delete operation. The task result contains a <see
    /// cref="ItemCheckResult"/> indicating the outcome of the deletion.</returns>
    Task<ItemCheckResult> DeleteItemsAsync(Guid userId, IList<Guid> itemIds, CancellationToken cancellationToken = default);
}
