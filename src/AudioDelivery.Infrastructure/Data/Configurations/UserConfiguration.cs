using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.JoinTables;
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

        builder.HasMany(u => u.SavedAlbums)
            .WithMany(a => a.SavedByUsers)
            .UsingEntity<UserSavedAlbum>(
                j => j.HasOne(usa => usa.Album)
                    .WithMany()
                    .HasForeignKey(usa => usa.AlbumId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(usa => usa.User)
                    .WithMany()
                    .HasForeignKey(usa => usa.UserId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(UserSavedAlbum))
            );

        builder.HasMany(u => u.SavedTracks)
            .WithMany(t => t.SavedByUsers)
            .UsingEntity<UserSavedTrack>(
                j => j.HasOne(ust => ust.Track)
                    .WithMany()
                    .HasForeignKey(ust => ust.TrackId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(ust => ust.User)
                    .WithMany()
                    .HasForeignKey(ust => ust.UserId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(UserSavedTrack))
            );

        // UserFollowedUser is self-referencing (both FKs point to Users).
        // SQL Server does not allow CASCADE on both FKs from the same table.
        // Follow relationships must be cleaned up explicitly in the service layer before deleting a user.
        builder.HasMany(u => u.Followers)
            .WithMany(u => u.FollowedUsers)
            .UsingEntity<UserFollowedUser>(
                j => j.HasOne(ufu => ufu.FollowedUser)
                    .WithMany()
                    .HasForeignKey(ufu => ufu.FollowedUserId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j.HasOne(ufu => ufu.Follower)
                    .WithMany()
                    .HasForeignKey(ufu => ufu.FollowerId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j.ToTable(nameof(UserFollowedUser))
            );

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasFilter("[Email] IS NOT NULL");

        builder.HasIndex(u => u.DisplayName);
    }
}
