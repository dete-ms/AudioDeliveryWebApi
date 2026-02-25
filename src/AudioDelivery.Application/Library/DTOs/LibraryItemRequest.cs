namespace AudioDelivery.Application.Library.DTOs;

/// <summary>
/// Request body for saving or removing items in the Library.
/// </summary>
public sealed class LibraryItemRequest
{
    /// <summary>
    /// A comma-separated list of Spotify URIs to save or remove (max 40).
    ///
    /// Examples:
    ///   "spotify:track:4iV5W9uYEdYUVa79Axb7Rh"
    ///   "spotify:album:1DFixLWuPkv3KT3TnV35m3"
    ///   "spotify:artist:0TnOYISbd1XYRBk9myaseg"
    ///   "spotify:playlist:37i9dQZF1DXcBWIGoYBM5M"
    /// </summary>
    public string Uris { get; init; } = string.Empty;
}
