namespace AudioDelivery.Application.Markets.DTOs;

/// <summary>
/// Market data returned by GET /api/v1/markets.
/// </summary>
public class MarketDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
