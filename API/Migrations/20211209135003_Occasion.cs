using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Occasion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occasions_AspNetUsers_UserId1",
                table: "Occasions");

            migrationBuilder.DropIndex(
                name: "IX_Occasions_UserId1",
                table: "Occasions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Occasions");

            migrationBuilder.CreateTable(
                name: "OccasionUser",
                columns: table => new
                {
                    OccasionsOccasionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersAttendingId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccasionUser", x => new { x.OccasionsOccasionId, x.UsersAttendingId });
                    table.ForeignKey(
                        name: "FK_OccasionUser_AspNetUsers_UsersAttendingId",
                        column: x => x.UsersAttendingId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OccasionUser_Occasions_OccasionsOccasionId",
                        column: x => x.OccasionsOccasionId,
                        principalTable: "Occasions",
                        principalColumn: "OccasionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OccasionUser_UsersAttendingId",
                table: "OccasionUser",
                column: "UsersAttendingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OccasionUser");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Occasions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_UserId1",
                table: "Occasions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Occasions_AspNetUsers_UserId1",
                table: "Occasions",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
