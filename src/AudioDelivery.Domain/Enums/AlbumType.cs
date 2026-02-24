namespace AudioDelivery.Domain.Enums;

/// <summary>
/// The type of an album, matching Spotify's album_type field.
/// See: https://developer.spotify.com/documentation/web-api/reference/get-an-album
///
/// Values:
///   Album       – A full-length album release
///   Single      – A single or EP release
///   Compilation – A compilation of tracks from various sources
/// </summary>
public enum AlbumType
{
    Album,
    Single,
    Compilation
}
