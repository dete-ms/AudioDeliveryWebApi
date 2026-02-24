using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Album-specific repository implementation.
///
/// TODO: Implement the methods defined in IAlbumRepository.
///       Use Include() / ThenInclude() for eager loading related data.
///       Use IQueryable for building complex queries with filtering and pagination.
/// </summary>
public class AlbumRepository : Repository<Album>, IAlbumRepository
{
    public AlbumRepository(AppDbContext context) : base(context) { }

    // TODO: Implement album-specific query methods
}
