using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;
using AutoMapper;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Category-specific repository implementation.
/// </summary>
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}
