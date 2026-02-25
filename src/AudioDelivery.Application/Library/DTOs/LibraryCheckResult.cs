namespace AudioDelivery.Application.Library.DTOs;

/// <summary>
/// Response for the library contains check endpoint.
/// </summary>
public sealed class LibraryCheckResult
{
    /// <summary>
    /// Ordered array of booleans indicating whether each URI is saved in the user's library.
    /// </summary>
    public IReadOnlyList<bool> Results { get; init; } = Array.Empty<bool>();
}
