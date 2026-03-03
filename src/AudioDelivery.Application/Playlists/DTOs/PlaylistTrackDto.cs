using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Application.Users.DTOs;

namespace AudioDelivery.Application.Playlists.DTOs;

public class PlaylistTrackDto
{
    public int Position { get; set; }
    public DateTime AddedAt { get; set; }
    public PublicUserDto? AddedBy { get; set; }
    public TrackDto Track { get; set; } = null!;
}