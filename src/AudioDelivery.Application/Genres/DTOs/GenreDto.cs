namespace AudioDelivery.Application.Genres.DTOs;

/// <summary>
/// Genre data returned by GET /api/v1/genres/seeds.
/// </summary>
public class GenreDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
