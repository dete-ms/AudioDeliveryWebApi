namespace AudioDelivery.Application.Library.DTOs;

/// <summary>
/// Request body for saving or removing items in the Library.
/// </summary>
public sealed class LibraryItemRequest
{
    /// <summary>
    /// A comma-separated list of uris to save or remove (max 40).
    /// </summary>
    public string Uris { get; init; } = string.Empty;
}
