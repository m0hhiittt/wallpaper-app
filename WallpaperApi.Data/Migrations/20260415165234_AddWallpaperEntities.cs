using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WallpaperApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWallpaperEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Creator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HashedPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WallpaperTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewProcess_Creator_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Creator",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wallpaper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WallpaperTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallpaper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallpaper_Creator_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Creator",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Resolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resolution720P = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resolution1080P = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resolution2K = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resolution4K = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WallpaperId = table.Column<int>(type: "int", nullable: true),
                    ReviewProcessId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resolutions_ReviewProcess_ReviewProcessId",
                        column: x => x.ReviewProcessId,
                        principalTable: "ReviewProcess",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Resolutions_Wallpaper_WallpaperId",
                        column: x => x.WallpaperId,
                        principalTable: "Wallpaper",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resolutions_ReviewProcessId",
                table: "Resolutions",
                column: "ReviewProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Resolutions_WallpaperId",
                table: "Resolutions",
                column: "WallpaperId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewProcess_CreatorId",
                table: "ReviewProcess",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallpaper_CreatorId",
                table: "Wallpaper",
                column: "CreatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resolutions");

            migrationBuilder.DropTable(
                name: "ReviewProcess");

            migrationBuilder.DropTable(
                name: "Wallpaper");

            migrationBuilder.DropTable(
                name: "Creator");
        }
    }
}
