using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Domain.JoinTables;
using AudioDelivery.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AudioDelivery.Application.Common.DTOs;

namespace AudioDelivery.Infrastructure.Repositories;

public class UserLibraryRepository : Repository<UserLibraryItem>, IUserLibraryRepository
{
    public UserLibraryRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    /// <inheritdoc />
    public async Task AddItemAsync(Guid userId, Guid entityId, EntityType entityType, CancellationToken cancellationToken = default)
    {
        var newItem = new UserLibraryItem
        {
            UserId = userId,
            TrackId = entityType == EntityType.Track ? entityId : null,
            AlbumId = entityType == EntityType.Album ? entityId : null,
            ArtistId = entityType == EntityType.Artist ? entityId : null,
            PlaylistId = entityType == EntityType.Playlist ? entityId : null,
        };
     
        await base.AddAsync(newItem);
    }

    /// <inheritdoc />
    public Task<bool> DeleteItemAsync(Guid userId, Guid itemId, CancellationToken cancellationToken = default)
    {
        var itemToRemove = base.Query().FirstOrDefault(u => u.UserId == userId &&
            (u.TrackId == itemId ||
            u.AlbumId == itemId ||
            u.ArtistId == itemId ||
            u.PlaylistId == itemId));

        if (itemToRemove == null)
        {
            return Task.FromResult(false);
        }

        base.Delete(itemToRemove);
        return Task.FromResult(true);
    }

    /// <inheritdoc />
    public Task<ItemCheckResult> DeleteItemsAsync(Guid userId, IList<Guid> itemIds, CancellationToken cancellationToken = default)
    {
        var itemsToRemove = base.Query().Where(u => u.UserId == userId &&
            (itemIds.Contains(u.TrackId ?? Guid.Empty) ||
            itemIds.Contains(u.AlbumId ?? Guid.Empty) ||
            itemIds.Contains(u.ArtistId ?? Guid.Empty) ||
            itemIds.Contains(u.PlaylistId ?? Guid.Empty)))
            .ToList();

        var checkResult = new ItemCheckResult();
        var results = new List<bool>();

        if (!itemsToRemove.Any())
        {
            return Task.FromResult(checkResult);
        }

        foreach (var item in itemsToRemove)
        {
            try
            {
                base.Delete(item);
                results.Add(true);
            }
            catch
            {
                results.Add(false);
            }
        }

        return Task.FromResult(checkResult);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteItemsRelatedToEntityAsync(Guid entityId, EntityType entitytype, CancellationToken cancellationToken = default)
    {
        var itemsToRemove = entitytype switch
        {
            EntityType.Track => await base.Query()
                .Where(u => u.TrackId == entityId)
                .ToListAsync(cancellationToken),

            EntityType.Album => await base.Query()
                .Where(u => u.AlbumId == entityId)
                .ToListAsync(cancellationToken),

            EntityType.Artist => await base.Query()
                .Where(u => u.ArtistId == entityId)
                .ToListAsync(cancellationToken),

            EntityType.Playlist => await base.Query()
                .Where(u => u.PlaylistId == entityId)
                .ToListAsync(cancellationToken),

            _ => new List<UserLibraryItem>()
        };

        try
        {
            foreach (var item in itemsToRemove)
            {
                base.Delete(item);
            }
        }
        catch
        {
            return false;
        }
        
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    /// <inheritdoc />
    public async Task<ItemCheckResult> AreItemsSavedAsync(Guid userId, IList<Guid> itemIds, CancellationToken cancellationToken = default)
    {
        var libraryItems = await base.Query().Where(u => u.UserId == userId).ToListAsync();
        var results = new List<bool>();

        if (libraryItems == null)
        {
            return new ItemCheckResult();
        }

        foreach (var itemId in itemIds)
        {
            results.Add(libraryItems.Any(i => 
                i.TrackId == itemId || 
                i.AlbumId == itemId || 
                i.ArtistId == itemId || 
                i.PlaylistId == itemId)
            );
        }

        return new ItemCheckResult { Results = results };
    }

    
    public Task<bool> IsItemSavedAsync(Guid userId, Guid itemId, CancellationToken cancellationToken = default)
    {
        return base.Query().AnyAsync(u => u.UserId == userId &&
            (u.TrackId == itemId ||
            u.AlbumId == itemId ||
            u.ArtistId == itemId ||
            u.PlaylistId == itemId),
            cancellationToken
        );
    }
}
