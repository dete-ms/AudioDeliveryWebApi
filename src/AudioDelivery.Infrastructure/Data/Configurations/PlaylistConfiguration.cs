using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Playlist entity.
///
/// TODO: Complete the configuration by defining:
///   - Table name, primary key, property constraints
///   - Many-to-one with User (Playlist.OwnerId → User.Id)
///   - One-to-many with PlaylistTrack
///   - One-to-many with Image (filtered to PlaylistId)
///   - Index on Name, OwnerId
/// </summary>
public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("Playlists");
        builder.HasKey(p => p.Id);

        // TODO: Configure properties – Name (required, max 256), Description, IsPublic, Collaborative
        // TODO: Configure Owner relationship (required FK)
        // TODO: Configure PlaylistTracks and Images relationships
        // TODO: Add indexes
    }
}
