using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Genre-specific repository implementation.
/// </summary>
public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(AppDbContext context) : base(context) { }

    // TODO: Implement genre-specific query methods
}
