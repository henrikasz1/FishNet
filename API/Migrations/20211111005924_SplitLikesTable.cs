using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class SplitLikesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.CreateTable(
                name: "PhotoLikes",
                columns: table => new
                {
                    ObjectId = table.Column<string>(type: "TEXT", nullable: false),
                    LoverId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoLikes", x => new { x.ObjectId, x.LoverId });
                });

            migrationBuilder.CreateTable(
                name: "PostLikes",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LoverId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikes", x => new { x.ObjectId, x.LoverId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoLikes");

            migrationBuilder.DropTable(
                name: "PostLikes");

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    ObjectId = table.Column<string>(type: "TEXT", nullable: false),
                    LoverId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.ObjectId, x.LoverId });
                });
        }
    }
}
