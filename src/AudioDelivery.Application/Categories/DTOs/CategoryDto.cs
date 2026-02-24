namespace AudioDelivery.Application.Categories.DTOs;

/// <summary>
/// Category data returned by browse category endpoints.
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-categories
/// </summary>
public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    // TODO: Add Images list
}
