using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AudioDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLibraryItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSavedAlbum");

            migrationBuilder.DropTable(
                name: "UserSavedTrack");

            migrationBuilder.AddColumn<long>(
                name: "PlayCount",
                table: "Tracks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "UserLibraryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLibraryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLibraryItems_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLibraryItems_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLibraryItems_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLibraryItems_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLibraryItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraryItems_AlbumId",
                table: "UserLibraryItems",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraryItems_ArtistId",
                table: "UserLibraryItems",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraryItems_PlaylistId",
                table: "UserLibraryItems",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraryItems_TrackId",
                table: "UserLibraryItems",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraryItems_UserId_AlbumId",
                table: "UserLibraryItems",
                columns: new[] { "UserId", "AlbumId" },
                unique: true,
                filter: "[AlbumId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraryItems_UserId_ArtistId",
                table: "UserLibraryItems",
                columns: new[] { "UserId", "ArtistId" },
                unique: true,
                filter: "[ArtistId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraryItems_UserId_PlaylistId",
                table: "UserLibraryItems",
                columns: new[] { "UserId", "PlaylistId" },
                unique: true,
                filter: "[PlaylistId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraryItems_UserId_TrackId",
                table: "UserLibraryItems",
                columns: new[] { "UserId", "TrackId" },
                unique: true,
                filter: "[TrackId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLibraryItems");

            migrationBuilder.DropColumn(
                name: "PlayCount",
                table: "Tracks");

            migrationBuilder.CreateTable(
                name: "UserSavedAlbum",
                columns: table => new
                {
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "UserSavedTrack",
                columns: table => new
                {
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_UserSavedAlbum_UserId",
                table: "UserSavedAlbum",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSavedTrack_UserId",
                table: "UserSavedTrack",
                column: "UserId");
        }
    }
}
