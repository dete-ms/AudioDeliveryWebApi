using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AudioDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryAndGenreDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b1000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Music", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Releases", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upcoming Releases", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Made For You", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Discover", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fresh Finds", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trending", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spotify Singles", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EQUAL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000010"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RADAR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mixed By", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000012"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charts", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Podcast Charts", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000014"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000015"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hip-Hop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000016"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000017"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Latin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000018"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dance/Electronic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Country", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000020"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "R&B", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000021"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "K-Pop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000022"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indie", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000023"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000024"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jazz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000025"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blues", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000026"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soul", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000027"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classical", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000028"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Punk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000029"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ambient", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000030"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Folk & Acoustic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000031"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Funk & Disco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000032"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alternative", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000033"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Afro", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000034"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Caribbean", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000035"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Instrumental", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000036"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mood", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000037"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chill", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000038"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sleep", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000039"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Party", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000040"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Workout", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000041"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Focus", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000042"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Love", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000043"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Summer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000044"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "At Home", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000045"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wellness", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000046"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nature & Noise", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000047"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Decades", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000048"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Travel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000049"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cooking & Dining", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000050"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gaming", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000051"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kids & Family", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000052"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Songwriters", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000053"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Podcasts", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000054"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Educational", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000055"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Documentary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000056"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comedy", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000057"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TV & Movies", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000058"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anime", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000059"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Netflix", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000060"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Live Events", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000061"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Radio", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b1000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alternative Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indie Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grunge", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Progressive Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Psychedelic Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Art Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Surf Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stoner Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000010"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shoegaze", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post-Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000012"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Math Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Britpop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000014"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000015"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heavy Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000016"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thrash Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000017"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Death Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000018"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Power Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000020"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Progressive Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000021"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Symphonic Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000022"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doom Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000023"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Folk Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000024"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Melodic Death Metal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000025"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Metalcore", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000026"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Punk Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000027"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hardcore Punk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000028"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post-Punk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000029"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post-Hardcore", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000030"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ska Punk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000031"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000032"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gothic Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000033"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000034"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Synth-Pop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000035"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Wave", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000036"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dance-Pop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000037"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Disco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000038"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Latin Pop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000039"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "K-Pop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000040"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "J-Pop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000041"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mandopop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000042"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hip-Hop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000043"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rap", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000044"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trap", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000045"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drill", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000046"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Boom Bap", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000047"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "G-Funk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000048"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Crunk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000049"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grime", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000050"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "R&B", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000051"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soul", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000052"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neo-Soul", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000053"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Jack Swing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000054"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Funk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000055"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electronic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000056"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "House", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000057"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deep House", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000058"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Progressive House", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000059"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Acid House", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000060"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Techno", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000061"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trance", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000062"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Psytrance", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000063"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dubstep", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000064"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drum and Bass", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000065"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hardstyle", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000066"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gabber", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000067"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hardcore", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000068"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UK Garage", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000069"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breakbeat", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000070"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electro", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000071"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electro Swing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000072"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IDM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000073"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EBM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000074"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Synthwave", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000075"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vaporwave", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000076"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Future Bass", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000077"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Glitch Hop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000078"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trip Hop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000079"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chillout", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000080"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lo-Fi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000081"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ambient", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000082"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jazz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000083"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blues", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000084"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soul Blues", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000085"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Country", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000086"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Folk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000087"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bluegrass", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000088"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Americana", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000089"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cajun", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000090"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zydeco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000091"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classical", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000092"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Opera", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000093"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baroque", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000094"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romantic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000095"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Impressionism", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000096"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Contemporary Classical", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000097"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minimalism", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000098"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avant-Garde", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000099"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Experimental", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000100"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reggae", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000101"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ska", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000102"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dancehall", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000103"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reggaeton", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000104"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salsa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000105"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachata", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000106"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Merengue", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000107"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumbia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000108"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flamenco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000109"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bossa Nova", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000110"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samba", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tango", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000112"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Afrobeat", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000113"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Afropop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000114"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Highlife", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000115"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rai", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000116"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Qawwali", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000117"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bollywood", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000118"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "World Music", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000119"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ethno", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000120"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gospel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000121"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christian Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000122"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Polka", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000123"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chanson", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000124"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Schlager", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000125"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soundtrack", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b1000000-0000-0000-0000-000000000126"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Industrial", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000052"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000053"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000054"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000055"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000056"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000057"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000058"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000059"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000061"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000052"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000053"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000054"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000055"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000056"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000057"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000058"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000059"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000061"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000062"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000063"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000064"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000065"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000066"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000067"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000068"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000069"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000070"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000071"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000072"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000073"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000074"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000075"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000076"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000077"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000078"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000079"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000080"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000081"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000082"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000083"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000084"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000085"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000086"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000087"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000088"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000089"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000090"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000091"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000092"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000093"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000094"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000095"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000096"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000097"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000098"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000099"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000100"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000101"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000102"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000103"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000104"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000105"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000106"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000107"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000108"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000109"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000110"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000111"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000112"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000113"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000114"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000115"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000116"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000117"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000118"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000119"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000120"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000121"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000122"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000123"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000124"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000125"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000126"));
        }
    }
}
