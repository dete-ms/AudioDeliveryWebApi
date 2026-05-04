using Microsoft.AspNetCore.Http;

namespace AudioDelivery.Application.Artists.DTOs;

public class UpdateArtistRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IList<string> Genres { get; set; } = new List<string>();
    public IFormFile? Image { get; set; }
}
