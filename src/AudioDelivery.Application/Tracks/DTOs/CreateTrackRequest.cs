namespace AudioDelivery.Application.Tracks.DTOs;

public class CreateTrackRequest
{
    public string Name { get; set; } = string.Empty;
    public int DiscNumber { get; set; }
    public int TrackNumber { get; set; }
    public int DurationMs { get; set; }
    public Guid? AlbumId { get; set; }
    public IList<Guid> ArtistIds { get; set; } = [];
}
