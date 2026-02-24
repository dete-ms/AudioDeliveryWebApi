using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Category-specific repository interface.
///
/// TODO: Add methods like:
///   - GetCategoryWithPlaylistsAsync(Guid id, int offset, int limit)
///   - GetCategoriesPaginatedAsync(string? country, string? locale, int offset, int limit)
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    // TODO: Define category-specific query method signatures here
}
