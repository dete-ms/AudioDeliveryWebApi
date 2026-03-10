using AudioDelivery.Application.Categories.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Playlists.DTOs;

namespace AudioDelivery.Application.Categories;

/// <summary>
/// Service interface for Browse Categories.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// GET /browse/categories – Get several categories.
    /// </summary>
    Task<PaginatedResult<CategoryDto>> GetCategoriesAsync(string? country = null, string? locale = null, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /browse/categories/{categoryId} – Get a single category.
    /// </summary>
    Task<CategoryDto?> GetCategoryAsync(Guid categoryId, string? country = null, string? locale = null, CancellationToken cancellationToken = default);
}
