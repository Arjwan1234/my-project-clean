using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GAMA.CO5.Migrations
{
    /// <inheritdoc />
    public partial class AddHomeAdminSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeAbouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BadgeText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stat1Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stat1Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stat2Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stat2Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stat3Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stat3Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeAbouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeHeroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BadgeText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeHeroes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeNews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeNews", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeAbouts");

            migrationBuilder.DropTable(
                name: "HomeHeroes");

            migrationBuilder.DropTable(
                name: "HomeNews");
        }
    }
}
