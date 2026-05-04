using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Application.Common.Services;

public class HrefGenerationService : IHrefGenerationService
{
    private readonly string _baseUrl = "/api/v1";

    public string GenerateHref(EntityType entityType)
    {
        var secondPart = entityType switch
        {
            EntityType.User => "/users/{id}",
            EntityType.Album => "/albums/{id}",
            EntityType.Genre => "/genres/{id}",
            EntityType.Image => "/images/{id}",
            EntityType.Track => "/tracks/{id}",
            EntityType.Artist => "/artists/{id}",
            EntityType.Category => "/categories/{id}",
            EntityType.Playlist => "/playlists/{id}",
            EntityType.PlaylistTrack => "/playlist-tracks/{id}",
            _ => throw new ArgumentOutOfRangeException(nameof(entityType), $"Unsupported entity type: {entityType}")
        };

        return _baseUrl + secondPart;
    }

    public string GeneratePaginatedHref(EntityType entityType, int limit = 50, int offset = 0)
    {
        return this.GenerateHref(entityType) + $"?offset={offset}&limit={limit}";
    }
}
