using AudioDelivery.Application.Images.DTOs;

namespace AudioDelivery.Application.Artists.DTOs;

public class CreateArtistRequest
{
    public string Name { get; set; } = string.Empty;
    public IList<string> Genres { get; set; } = new List<string>();
    public IList<CreateImageRequest> Images { get; set; } = [];
}
