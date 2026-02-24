using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the User entity.
///
/// TODO: Complete the configuration by defining:
///   - Table name, primary key, property constraints
///   - One-to-many with Playlist
///   - One-to-many with Image (filtered to UserId)
///   - Unique index on Email (when not null)
///   - Index on DisplayName
///
/// NOTE: Authentication fields will be added later when we integrate Identity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        // TODO: Configure properties – DisplayName, Email, Country, Uri
        // TODO: Configure relationships (Playlists, Images)
        // TODO: Add unique index on Email
    }
}
