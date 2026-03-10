namespace AudioDelivery.Application.Categories.DTOs;

/// <summary>
/// Category data returned by browse category endpoints.
/// </summary>
public class CategorySummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
