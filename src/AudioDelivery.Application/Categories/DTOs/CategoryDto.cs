using AudioDelivery.Application.Images.DTOs;

namespace AudioDelivery.Application.Categories.DTOs;

/// <summary>
/// Category data returned by browse category endpoints.
/// </summary>
public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IList<ImageDto> Images { get; set; } = new List<ImageDto>();
}
