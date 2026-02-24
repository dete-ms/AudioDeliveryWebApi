using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the AudioFeatures entity.
///
/// TODO: Complete the configuration by defining:
///   - Table name "AudioFeatures", primary key
///   - One-to-one relationship with Track (TrackId as FK, unique)
///   - All float properties with appropriate precision
/// </summary>
public class AudioFeaturesConfiguration : IEntityTypeConfiguration<AudioFeatures>
{
    public void Configure(EntityTypeBuilder<AudioFeatures> builder)
    {
        builder.ToTable("AudioFeatures");
        builder.HasKey(af => af.Id);

        // TODO: Configure one-to-one with Track
        // TODO: Configure float property precisions if needed
    }
}
