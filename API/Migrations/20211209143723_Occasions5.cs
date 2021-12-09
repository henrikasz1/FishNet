using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Occasions5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occasions_OccasionOwner_OwnerId",
                table: "Occasions");

            migrationBuilder.DropTable(
                name: "OccasionOwner");

            migrationBuilder.DropTable(
                name: "OccasionUser");

            migrationBuilder.DropIndex(
                name: "IX_Occasions_OwnerId",
                table: "Occasions");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Occasions",
                newName: "UserId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Occasions",
                newName: "OwnerId");

            migrationBuilder.CreateTable(
                name: "OccasionOwner",
                columns: table => new
                {
                    OccasionOwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccasionOwner", x => x.OccasionOwnerId);
                    table.ForeignKey(
                        name: "FK_OccasionOwner_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Occasions_OwnerId",
                table: "Occasions",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OccasionOwner_UserId1",
                table: "OccasionOwner",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_OccasionUser_UsersAttendingId",
                table: "OccasionUser",
                column: "UsersAttendingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occasions_OccasionOwner_OwnerId",
                table: "Occasions",
                column: "OwnerId",
                principalTable: "OccasionOwner",
                principalColumn: "OccasionOwnerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
