using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Market-specific repository interface.
///
/// TODO: Add methods like:
///   - GetAvailableMarketsAsync() – returns all ISO country codes
/// </summary>
public interface IMarketRepository : IRepository<Market>
{
    // TODO: Define market-specific query method signatures here
}
