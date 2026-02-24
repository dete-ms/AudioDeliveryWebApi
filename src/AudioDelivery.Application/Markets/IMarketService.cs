using AudioDelivery.Application.Markets.DTOs;

namespace AudioDelivery.Application.Markets;

/// <summary>
/// Service interface for Market operations.
/// </summary>
public interface IMarketService
{
    /// <summary>
    /// GET /markets – Get available markets (countries).
    /// </summary>
    Task<IReadOnlyList<MarketDto>> GetAvailableMarketsAsync(CancellationToken cancellationToken = default);
}
