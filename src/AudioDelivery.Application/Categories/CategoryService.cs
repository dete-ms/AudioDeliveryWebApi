using AudioDelivery.Application.Categories.DTOs;
using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AudioDelivery.Application.Categories;

/// <summary>
/// Category service implementation.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _repository = categoryRepository;
        _mapper = mapper;
    }

    public Task<PaginatedResult<CategoryDto>> GetCategoriesAsync(string? country = null, string? locale = null, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, this.GetHref(offset, limit), cancellationToken);
    }

    public Task<CategoryDto?> GetCategoryAsync(Guid id, string? country = null, string? locale = null, CancellationToken cancellationToken = default)
    {
        return _repository.FindFirstAsync<CategoryDto>(c => c.Id == id);
    }

    public async Task DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdTrackedAsync(id, cancellationToken);

        if (category == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found.");
        }

        _repository.Delete(category);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    private string GetHref(int offset, int limit) => $"/api/v1/categories?offset={offset}&limit={limit}";
}
