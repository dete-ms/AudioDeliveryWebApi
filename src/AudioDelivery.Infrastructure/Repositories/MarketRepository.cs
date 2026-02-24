using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Market-specific repository implementation.
/// </summary>
public class MarketRepository : Repository<Market>, IMarketRepository
{
    public MarketRepository(AppDbContext context) : base(context) { }

    // TODO: Implement market-specific query methods
}
