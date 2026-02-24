using AudioDelivery.Application.Albums;
using AudioDelivery.Application.Artists;
using AudioDelivery.Application.Categories;
using AudioDelivery.Application.Genres;
using AudioDelivery.Application.Playlists;
using AudioDelivery.Application.Search;
using AudioDelivery.Application.Tracks;
using AudioDelivery.Application.Users;

namespace AudioDelivery.Api.Extensions;

/// <summary>
/// Registers all Application-layer services into the DI container.
///
/// PURPOSE:
/// Keeps Program.cs clean by grouping service registrations in one place.
/// Each service is registered as Scoped (one instance per HTTP request),
/// which aligns with the scoped lifetime of DbContext.
///
/// PATTERN:
/// Interface → Implementation mapping allows controllers to depend on
/// abstractions, making them testable and loosely coupled.
///
/// PHASE GUIDE:
/// - Phase 6: Implement each service method
/// - Phase 10: Add caching decorators on top of these registrations
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register each domain service as Scoped
        // Scoped = one instance per HTTP request, matching DbContext's lifetime
        services.AddScoped<IAlbumService, AlbumService>();
        services.AddScoped<IArtistService, ArtistService>();
        services.AddScoped<ITrackService, TrackService>();
        services.AddScoped<IPlaylistService, PlaylistService>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
