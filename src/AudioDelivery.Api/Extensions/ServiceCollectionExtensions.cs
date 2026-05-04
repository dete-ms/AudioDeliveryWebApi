using AudioDelivery.Application.Albums;
using AudioDelivery.Application.Albums.Profiles;
using AudioDelivery.Application.Artists;
using AudioDelivery.Application.Categories;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Services;
using AudioDelivery.Application.Events.Handlers;
using AudioDelivery.Application.Genres;
using AudioDelivery.Application.Library;
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
        var luckyLicenseKey = "Add Lucky Penny Software license here";

        services.AddAutoMapper(cfg => { cfg.LicenseKey = luckyLicenseKey; }, 
            typeof(AlbumProfile).Assembly);

        services.AddMediatR(cfg => 
        { 
            cfg.LicenseKey = luckyLicenseKey;
            cfg.RegisterServicesFromAssembly(typeof(BlobCleanupHandler).Assembly);
        });

        services.AddSingleton<IUriGenerationService, UriGenerationService>();
        services.AddSingleton<IHrefGenerationService, HrefGenerationService>();

        services.AddScoped<IAlbumService, AlbumService>();
        services.AddScoped<IArtistService, ArtistService>();
        services.AddScoped<ITrackService, TrackService>();
        services.AddScoped<IPlaylistService, PlaylistService>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserLibraryService, UserLibraryService>();

        return services;
    }
}
