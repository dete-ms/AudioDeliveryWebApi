using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Playlist entity.
/// </summary>
public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("Playlists");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Description)
            .HasMaxLength(300);

        builder.Property(p => p.IsPublic)
            .HasDefaultValue(false);

        builder.Property(p => p.Collaborative)
            .HasDefaultValue(false);

        builder.Property(p => p.SnapshotId)
            .HasMaxLength(100);

        builder.Property(p => p.Uri)
            .HasMaxLength(200);

        builder.Property(p => p.ExternalUrl)
            .HasMaxLength(500);

        builder.HasMany(p => p.PlaylistTracks)
            .WithOne(pt => pt.Playlist)
            .HasForeignKey(pt => pt.PlaylistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Images)
            .WithOne(i => i.Playlist)
            .HasForeignKey(i => i.PlaylistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.OwnerId);
    }
}
