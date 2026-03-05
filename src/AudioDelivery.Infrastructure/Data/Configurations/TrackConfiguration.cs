using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Track entity.
/// </summary>
public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.ToTable("Tracks");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.DiscNumber)
            .HasDefaultValue(1);

        builder.Property(t => t.DurationMs)
            .IsRequired();

        builder.Property(t => t.Explicit)
            .HasDefaultValue(false);

        builder.Property(t => t.Popularity)
            .HasDefaultValue(0);

        builder.Property(t => t.PreviewUrl)
            .HasMaxLength(200);

        builder.Property(t => t.IsLocal)
            .HasDefaultValue(false);

        builder.Property(t => t.Uri)
            .HasMaxLength(200);

        builder.Property(t => t.ExternalUrl)
            .HasMaxLength(500);

        builder.HasIndex(t => t.Name);
        builder.HasIndex(t => t.AlbumId);

    }
}
