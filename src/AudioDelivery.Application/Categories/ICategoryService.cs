using AudioDelivery.Application.Categories.DTOs;
using AudioDelivery.Application.Common.Models;

namespace AudioDelivery.Application.Categories;

/// <summary>
/// Service interface for Browse Categories. Only admins should be able to create, update, or delete categories, while all users should be able to read them.
/// Categories are generated through the DataSeeder or directly in the database, so no create/update/delete operations are exposed through the API. 
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
    Task<CategoryDto?> GetCategoryAsync(Guid id, string? country = null, string? locale = null, CancellationToken cancellationToken = default);
}
