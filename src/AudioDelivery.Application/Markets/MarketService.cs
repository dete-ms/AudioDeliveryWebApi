using AudioDelivery.Application.Markets.DTOs;
using AudioDelivery.Infrastructure.Repositories;

namespace AudioDelivery.Application.Markets;

/// <summary>
/// Market service implementation.
/// </summary>
public class MarketService : IMarketService
{
    private readonly IMarketRepository _marketRepository;

    public MarketService(IMarketRepository marketRepository)
    {
        _marketRepository = marketRepository;
    }

    public async Task<IReadOnlyList<MarketDto>> GetAvailableMarketsAsync(CancellationToken cancellationToken = default)
    {
        // TODO: Implement – fetch all markets, map to MarketDto
        throw new NotImplementedException("Implement in Phase 6");
    }
}
