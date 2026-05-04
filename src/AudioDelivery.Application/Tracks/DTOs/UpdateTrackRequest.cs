namespace AudioDelivery.Application.Tracks.DTOs;

public class UpdateTrackRequest
{
    public string Name { get; set; } = string.Empty;

    public IList<Guid> ArtistIds { get; set; } = null!;
}
