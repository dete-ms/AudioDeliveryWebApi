using AudioDelivery.Application.Categories.DTOs;
using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Playlists.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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

    public Task<CategoryDto?> GetCategoryAsync(Guid categoryId, string? country = null, string? locale = null, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(c => c.Id == categoryId)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    private string GetHref(int offset, int limit) => $"/api/v1/categories?offset={offset}&limit={limit}";
}
