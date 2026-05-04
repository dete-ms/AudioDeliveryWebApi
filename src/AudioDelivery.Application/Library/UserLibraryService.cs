using AudioDelivery.Application.Common.DTOs;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Library.DTOs;

namespace AudioDelivery.Application.Library;

/// <inheritdoc />
public sealed class UserLibraryService : IUserLibraryService
{
    private readonly IUserLibraryRepository _repository;
    private readonly IUriGenerationService _uriGenerationService;

    public UserLibraryService(IUserLibraryRepository repository, IUriGenerationService uriGenerationService)
    {
        _repository = repository;
        _uriGenerationService = uriGenerationService;
    }

    /// <inheritdoc />
    public async Task SaveItemsAsync(Guid userId, LibraryItemRequest request, CancellationToken cancellationToken = default)
    {
        var uris = request.Uris.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var ids = ParseIdsFromRequest(request);

        var savedItems = await _repository.AreItemsSavedAsync(userId, ids, cancellationToken);

        var counter = 0;
        foreach (var savedItem in savedItems.Results)
        {
            if (!savedItem)
            {
                var (entityType, id) = _uriGenerationService.ParseUri(uris[counter]);
                await _repository.AddItemAsync(userId, id, entityType, cancellationToken);
            }

            counter++;
        }

        await _repository.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task RemoveItemsAsync(Guid userId, LibraryItemRequest request, CancellationToken cancellationToken = default)
    {
        var uris = request.Uris.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var ids = ParseIdsFromRequest(request);

        await _repository.DeleteItemsAsync(userId, ids, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<ItemCheckResult> CheckItemsAsync(Guid userId, LibraryItemRequest request, CancellationToken cancellationToken = default)
    {
        return _repository.AreItemsSavedAsync(userId, ParseIdsFromRequest(request), cancellationToken);
    }

    private IList<Guid> ParseIdsFromRequest(LibraryItemRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Uris))
            return new List<Guid>();

        var uris = request.Uris.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var ids = new List<Guid>();

        foreach (var uri in uris)
        {
            if (_uriGenerationService.IsValidUri(uri))
            {
                var (entityType, id) = _uriGenerationService.ParseUri(uri);
                ids.Add(id);
            }
        }

        return ids;
    }
}
