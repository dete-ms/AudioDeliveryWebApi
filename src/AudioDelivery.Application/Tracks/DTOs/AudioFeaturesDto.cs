namespace AudioDelivery.Application.Tracks.DTOs;

/// <summary>
/// Audio features for a track, returned by GET /api/v1/audio-features/{id}.
/// Maps to Spotify's AudioFeaturesObject.
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-audio-features
/// </summary>
public class AudioFeaturesDto
{
    public Guid Id { get; set; }
    public Guid TrackId { get; set; }
    public float Danceability { get; set; }
    public float Energy { get; set; }
    public int Key { get; set; }
    public float Loudness { get; set; }
    public int Mode { get; set; }
    public float Speechiness { get; set; }
    public float Acousticness { get; set; }
    public float Instrumentalness { get; set; }
    public float Liveness { get; set; }
    public float Valence { get; set; }
    public float Tempo { get; set; }
    public int TimeSignature { get; set; }
    public int DurationMs { get; set; }
    public string Uri { get; set; } = string.Empty;
}
