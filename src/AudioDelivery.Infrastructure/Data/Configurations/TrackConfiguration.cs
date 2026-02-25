using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Track entity.
///
/// TODO: Complete the configuration by defining:
///   - Table name, primary key, property constraints
///   - Many-to-one with Album (Track.AlbumId → Album.Id)
///   - Many-to-many with Artist (join table "ArtistTrack")
///   - One-to-many with PlaylistTrack
///   - Index on Name, AlbumId for query performance
/// </summary>
public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.ToTable("Tracks");
        builder.HasKey(t => t.Id);

        // TODO: Configure properties – Name (required), DurationMs, TrackNumber, etc.
        // TODO: Configure Album relationship (required FK)
        // TODO: Configure many-to-many relationship with Artists
        // TODO: Add indexes
    }
}
