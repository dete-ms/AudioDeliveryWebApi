using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Events.Handlers;
using AudioDelivery.Infrastructure.Repositories;
using AudioDelivery.Infrastructure.Storage;
using AudioDelivery.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Infrastructure.Extensions;

/// <summary>
/// Extension methods for registering Infrastructure layer services in DI.
///
/// This keeps the DI registration logic organized and out of Program.cs.
/// Call builder.Services.AddInfrastructure(builder.Configuration) from Program.cs.
/// </summary>
public static class InfrastructureServiceExtensions
{
    /// <summary>
    /// Registers the AppDbContext (with SQL Server) and all repository implementations.
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var sqlServerConnectionString = configuration.GetConnectionString("SqlServerConnectionString");
        var sqlServerConnectionString2 = configuration.GetConnectionString(AzureBlobStorageOptions.ConnectionStringName);

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(sqlServerConnectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            });

            // Fallback: InMemory database for quick testing without SQL Server
            // options.UseInMemoryDatabase("AudioDeliveryDb");
            // also, install the EntityFrameworkCore.InMemory package on this project for this to work
        });

        services.AddHostedService<OrphanBlobCleanupJob>();

        services.Configure<AzureBlobStorageOptions>(a => 
            a.ConnectionString = configuration.GetConnectionString(AzureBlobStorageOptions.ConnectionStringName) ?? string.Empty);

        services.AddSingleton<IStorageService, AzureBlobStorageService>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<ITrackRepository, TrackRepository>();
        services.AddScoped<IPlaylistRepository, PlaylistRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserLibraryRepository, UserLibraryRepository>();



        return services;
    }
}
