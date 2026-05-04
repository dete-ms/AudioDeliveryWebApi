using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// Service for generating standardized URIs for entities.
/// </summary>
public interface IUriGenerationService
{
    /// <summary>
    /// Generates a URI for the specified entity type and ID.
    /// Format: audiodelivery:{entityType}:{id}
    /// </summary>
    /// <param name="entityType">The type of the entity.</param>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>A standardized URI string.</returns>
    string GenerateUri(EntityType entityType, Guid id);

    /// <summary>
    /// Parses a URI string into its component parts.
    /// </summary>
    /// <param name="uri">The URI to parse.</param>
    /// <returns>A tuple containing the entity type and ID.</returns>
    /// <exception cref="ArgumentException">Thrown if the URI format is invalid.</exception>
    (EntityType EntityType, Guid Id) ParseUri(string uri);

    /// <summary>
    /// Validates whether a string is a valid AudioDelivery URI.
    /// </summary>
    /// <param name="uri">The URI to validate.</param>
    /// <returns>True if the URI is valid; otherwise, false.</returns>
    bool IsValidUri(string uri);
}