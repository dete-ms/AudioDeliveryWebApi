using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;
using AutoMapper;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Genre-specific repository implementation.
/// </summary>
public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}
