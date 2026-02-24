using AudioDelivery.Application.Categories.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Infrastructure.Repositories;

namespace AudioDelivery.Application.Categories;

/// <summary>
/// Category service implementation.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<PaginatedResult<CategoryDto>> GetCategoriesAsync(string? country = null, string? locale = null, int offset = 0, int limit = 20, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<CategoryDto?> GetCategoryAsync(Guid categoryId, string? country = null, string? locale = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<PaginatedResult<PlaylistSummaryDto>> GetCategoryPlaylistsAsync(Guid categoryId, string? country = null, int offset = 0, int limit = 20, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }
}
