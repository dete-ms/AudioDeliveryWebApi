using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Market entity.
///
/// TODO: Complete the configuration by defining:
///   - Table name, primary key
///   - Code property (required, max 2, unique index)
///   - Name property (required, max 100)
///   - Many-to-many relationships with Album and Track are configured
///     in their respective configuration classes.
/// </summary>
public class MarketConfiguration : IEntityTypeConfiguration<Market>
{
    public void Configure(EntityTypeBuilder<Market> builder)
    {
        builder.ToTable("Markets");
        builder.HasKey(m => m.Id);

        // TODO: Configure Code (required, max length 2) and add unique index
        // TODO: Configure Name (required)
    }
}
