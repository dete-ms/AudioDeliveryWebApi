using AudioDelivery.Application.Library.DTOs;

namespace AudioDelivery.Application.Library;

/// <summary>
/// Implements the unified Library service.
/// </summary>
public sealed class LibraryService : ILibraryService
{
    // TODO: Inject IUserLibraryRepository (or IRepository<UserLibraryItem>) when the
    //       repository layer is implemented in Phase 6.

    public LibraryService()
    {
        // TODO: Accept repository dependency via constructor injection.
    }

    /// <inheritdoc />
    public async Task SaveItemsAsync(Guid userId, LibraryItemRequest request, CancellationToken cancellationToken = default)
    {
        // TODO: Implement
        // 1. Parse comma-separated URIs from request.Uris (max 40)
        // 2. Validate each URI format: spotify:{type}:{id}
        // 3. For each URI, upsert a UserLibraryItem row (userId, uri, savedAt = UtcNow)
        // 4. Return (void / 200 OK on success)
        throw new NotImplementedException("Implement in Phase 6 – see docs/Phase06-ServiceLayer.md");
    }

    /// <inheritdoc />
    public async Task RemoveItemsAsync(Guid userId, LibraryItemRequest request, CancellationToken cancellationToken = default)
    {
        // TODO: Implement
        // 1. Parse comma-separated URIs from request.Uris (max 40)
        // 2. Delete matching UserLibraryItem rows for this userId
        // 3. Return (void / 200 OK on success)
        throw new NotImplementedException("Implement in Phase 6");
    }

    /// <inheritdoc />
    public async Task<LibraryCheckResult> CheckItemsAsync(Guid userId, string uris, CancellationToken cancellationToken = default)
    {
        // TODO: Implement
        // 1. Parse comma-separated URIs (max 40)
        // 2. Query UserLibraryItem for rows matching (userId, uri) pairs
        // 3. Return ordered bool[] – true if found, false if not
        throw new NotImplementedException("Implement in Phase 6");
    }
}
