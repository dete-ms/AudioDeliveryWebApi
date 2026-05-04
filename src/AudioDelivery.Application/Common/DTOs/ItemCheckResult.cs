namespace AudioDelivery.Application.Common.DTOs;

/// <summary>
/// Response for the library contains check endpoint.
/// </summary>
public sealed class ItemCheckResult
{
    /// <summary>
    /// Ordered array of booleans indicating whether each URI is saved in the user's library.
    /// </summary>
    public IReadOnlyList<bool> Results { get; init; } = Array.Empty<bool>();
}
