using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Artist entity.
/// </summary>
public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.ToTable("Artists");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Popularity)
            .HasDefaultValue(0);

        builder.Property(a => a.FollowerCount)
            .HasDefaultValue(0);

        builder.Property(a => a.Uri)
            .HasMaxLength(200);

        builder.Property(a => a.ExternalUrl)
            .HasMaxLength(500);

        builder.HasMany(a => a.Albums)
            .WithMany(al => al.Artists)
            .UsingEntity(j => j.ToTable("ArtistAlbum"));

        builder.HasMany(a => a.Tracks)
            .WithMany(t => t.Artists)
            .UsingEntity(j => j.ToTable("ArtistTrack"));

        builder.HasMany(a => a.Genres)
            .WithMany(g => g.Artists)
            .UsingEntity(j => j.ToTable("ArtistGenre"));

        builder.HasMany(a => a.Followers)
            .WithMany(u => u.FollowedArtists)
            .UsingEntity(j => j.ToTable("ArtistFollower"));

        builder.HasMany(a => a.Images)
            .WithOne(i => i.Artist)
            .HasForeignKey(i => i.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => a.Name);
    }
}
