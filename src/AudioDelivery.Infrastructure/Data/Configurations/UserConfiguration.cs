using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the User entity.
///
/// DESIGN NOTE — Artist vs User:
/// Artist deliberately does NOT inherit from User. Artists are music catalog entities
/// (acts, bands, historical artists) that may not have a user account. Users are account/
/// security principals with Email, Country, and personal library. The overlapping fields
/// (Uri, ExternalUrl, FollowerCount) are coincidental naming — different domain objects.
/// If an artist has a user account they'll be linked via a nullable UserId FK on Artist,
/// added in Phase 8 (Authentication).
///
/// TODO: Authentication fields (password hash, refresh tokens, roles) will be added
/// when integrating ASP.NET Core Identity in Phase 8.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.DisplayName)
            .HasMaxLength(200);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Country)
            .HasMaxLength(10);

        builder.Property(u => u.Uri)
            .HasMaxLength(200);

        builder.Property(u => u.ExternalUrl)
            .HasMaxLength(200);

        builder.HasMany(u => u.Playlists)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Images)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.SavedAlbums)
            .WithMany(a => a.SavedByUsers)
            .UsingEntity(j => j.ToTable("UserSavedAlbum"));

        builder.HasMany(u => u.SavedTracks)
            .WithMany(t => t.SavedByUsers)
            .UsingEntity(j => j.ToTable("UserSavedTrack"));

        builder.HasMany(u => u.Followers)
            .WithMany(u => u.FollowedUsers)
            .UsingEntity(j => j.ToTable("UserFollowedUser"));

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasFilter("[Email] IS NOT NULL");

        builder.HasIndex(u => u.DisplayName);
    }
}
