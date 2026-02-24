using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Artist entity.
///
/// TODO: Complete the configuration by defining:
///   - Table name, primary key, property constraints
///   - Many-to-many with Album (shared join table "ArtistAlbum")
///   - Many-to-many with Track (join table "ArtistTrack")
///   - Many-to-many with Genre (join table "ArtistGenre")
///   - One-to-many with Image (filtered to ArtistId)
///   - Index on Name for search performance
/// </summary>
public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.ToTable("Artists");
        builder.HasKey(a => a.Id);

        // TODO: Configure properties – Name (required, max length 256), Popularity, FollowerCount, Uri
        // TODO: Configure many-to-many relationships
        // TODO: Add indexes
    }
}
