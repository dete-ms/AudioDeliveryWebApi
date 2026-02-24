using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Genre-specific repository interface.
///
/// TODO: Add methods like:
///   - GetAvailableGenreSeedsAsync() – returns all genre names for recommendations
/// </summary>
public interface IGenreRepository : IRepository<Genre>
{
    // TODO: Define genre-specific query method signatures here
}
