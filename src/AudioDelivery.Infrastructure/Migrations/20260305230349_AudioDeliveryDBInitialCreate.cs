using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AudioDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AudioDeliveryDBInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AlbumType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReleaseDate = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ReleaseDatePrecision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Popularity = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExternalUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Popularity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FollowerCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Uri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExternalUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExternalUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DiscNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    TrackNumber = table.Column<int>(type: "int", nullable: false),
                    DurationMs = table.Column<int>(type: "int", nullable: false),
                    Explicit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Popularity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PreviewUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsLocal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Uri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExternalUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistAlbum",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistAlbum", x => new { x.AlbumId, x.ArtistId });
                    table.ForeignKey(
                        name: "FK_ArtistAlbum_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistAlbum_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistGenre",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistGenre", x => new { x.ArtistId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_ArtistGenre_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistFollower",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistFollower", x => new { x.ArtistId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ArtistFollower_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistFollower_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Collaborative = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SnapshotId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExternalUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFollowedUser",
                columns: table => new
                {
                    FollowerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowedUser", x => new { x.FollowedUserId, x.FollowerId });
                    table.ForeignKey(
                        name: "FK_UserFollowedUser_Users_FollowedUserId",
                        column: x => x.FollowedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFollowedUser_Users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSavedAlbum",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSavedAlbum", x => new { x.AlbumId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserSavedAlbum_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSavedAlbum_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistTrack",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistTrack", x => new { x.ArtistId, x.TrackId });
                    table.ForeignKey(
                        name: "FK_ArtistTrack_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistTrack_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSavedTrack",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSavedTrack", x => new { x.TrackId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserSavedTrack_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSavedTrack_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPlaylist",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPlaylist", x => new { x.CategoryId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_CategoryPlaylist_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPlaylist_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Width = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlaylistTracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistTracks_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistTracks_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistTracks_Users_AddedByUserId",
                        column: x => x.AddedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_AlbumType",
                table: "Albums",
                column: "AlbumType");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_Name",
                table: "Albums",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistAlbum_ArtistId",
                table: "ArtistAlbum",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistFollower_UserId",
                table: "ArtistFollower",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistGenre_GenreId",
                table: "ArtistGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_Name",
                table: "Artists",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistTrack_TrackId",
                table: "ArtistTrack",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPlaylist_PlaylistId",
                table: "CategoryPlaylist",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AlbumId",
                table: "Images",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArtistId",
                table: "Images",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CategoryId",
                table: "Images",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PlaylistId",
                table: "Images",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Name",
                table: "Playlists",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_OwnerId",
                table: "Playlists",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistTracks_AddedByUserId",
                table: "PlaylistTracks",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistTracks_PlaylistId_Position",
                table: "PlaylistTracks",
                columns: new[] { "PlaylistId", "Position" });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistTracks_TrackId",
                table: "PlaylistTracks",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_AlbumId",
                table: "Tracks",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_Name",
                table: "Tracks",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowedUser_FollowerId",
                table: "UserFollowedUser",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DisplayName",
                table: "Users",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserSavedAlbum_UserId",
                table: "UserSavedAlbum",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSavedTrack_UserId",
                table: "UserSavedTrack",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistAlbum");

            migrationBuilder.DropTable(
                name: "ArtistFollower");

            migrationBuilder.DropTable(
                name: "ArtistGenre");

            migrationBuilder.DropTable(
                name: "ArtistTrack");

            migrationBuilder.DropTable(
                name: "CategoryPlaylist");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "PlaylistTracks");

            migrationBuilder.DropTable(
                name: "UserFollowedUser");

            migrationBuilder.DropTable(
                name: "UserSavedAlbum");

            migrationBuilder.DropTable(
                name: "UserSavedTrack");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Albums");
        }
    }
}
