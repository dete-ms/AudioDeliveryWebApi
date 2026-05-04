using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Application.Common.Services;

/// <summary>
/// Default implementation of the URI generation service.
/// </summary>
public sealed class UriGenerationService : IUriGenerationService
{
    private const string UriScheme = "audiodelivery";

    /// <inheritdoc />
    public string GenerateUri(EntityType entityType, Guid id)
    {
        if (entityType == EntityType.Unknown)
            throw new ArgumentException("Cannot generate URI for Unknown entity type.", nameof(entityType));

        var typeName = entityType.ToString().ToLowerInvariant();
        return $"{UriScheme}:{typeName}:{id}";
    }

    /// <inheritdoc />
    public (EntityType EntityType, Guid Id) ParseUri(string uri)
    {
        if (string.IsNullOrWhiteSpace(uri))
            throw new ArgumentException("URI cannot be null or empty.", nameof(uri));

        var parts = uri.Trim().Split(':');

        if (parts.Length != 3)
            throw new ArgumentException($"Invalid URI format: '{uri}'. Expected '{UriScheme}:{{type}}:{{id}}'.");

        if (!parts[0].Equals(UriScheme, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException($"Invalid URI scheme: '{parts[0]}'. Expected '{UriScheme}'.");

        if (!Enum.TryParse<EntityType>(parts[1], ignoreCase: true, out var entityType) || entityType == EntityType.Unknown)
            throw new ArgumentException($"Unknown entity type: '{parts[1]}'.");

        if (!Guid.TryParse(parts[2], out var id))
            throw new ArgumentException($"Invalid GUID format: '{parts[2]}'.");

        return (entityType, id);
    }

    /// <inheritdoc />
    public bool IsValidUri(string uri)
    {
        if (string.IsNullOrWhiteSpace(uri))
            return false;

        var parts = uri.Trim().Split(':');

        if (parts.Length != 3)
            return false;

        if (!parts[0].Equals(UriScheme, StringComparison.OrdinalIgnoreCase))
            return false;

        if (!Enum.TryParse<EntityType>(parts[1], ignoreCase: true, out var entityType) || entityType == EntityType.Unknown)
            return false;

        if (!Guid.TryParse(parts[2], out _))
            return false;

        return true;
    }
}