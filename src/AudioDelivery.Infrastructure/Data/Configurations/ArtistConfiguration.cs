using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.JoinTables;
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
            .UsingEntity<ArtistAlbum>(
                j => j.HasOne(aa => aa.Album)
                    .WithMany()
                    .HasForeignKey(aa => aa.AlbumId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(aa => aa.Artist)
                    .WithMany()
                    .HasForeignKey(aa => aa.ArtistId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(ArtistAlbum))
            );

        builder.HasMany(a => a.Tracks)
            .WithMany(t => t.Artists)
            .UsingEntity<ArtistTrack>(
                j => j.HasOne(at => at.Track)
                    .WithMany()
                    .HasForeignKey(at => at.TrackId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(at => at.Artist)
                    .WithMany()
                    .HasForeignKey(at => at.ArtistId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(ArtistTrack))
            );

        builder.HasMany(a => a.Genres)
            .WithMany(g => g.Artists)
            .UsingEntity<ArtistGenre>(
                j => j.HasOne(ag => ag.Genre)
                    .WithMany()
                    .HasForeignKey(ag => ag.GenreId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(ag => ag.Artist)
                    .WithMany()
                    .HasForeignKey(ag => ag.ArtistId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(ArtistGenre))
            );

        builder.HasMany(a => a.Followers)
            .WithMany(u => u.FollowedArtists)
            .UsingEntity<ArtistFollower>(
                j => j.HasOne(af => af.User)
                    .WithMany()
                    .HasForeignKey(af => af.UserId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(af => af.Artist)
                    .WithMany()
                    .HasForeignKey(af => af.ArtistId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(ArtistFollower))
            );

        builder.HasIndex(a => a.Name);
    }
}
