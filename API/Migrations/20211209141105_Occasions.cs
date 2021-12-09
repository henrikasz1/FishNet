using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Occasions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerOccasionOwnerId",
                table: "Occasions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OccasionOwner",
                columns: table => new
                {
                    OccasionOwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccasionOwner", x => x.OccasionOwnerId);
                    table.ForeignKey(
                        name: "FK_OccasionOwner_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_OwnerOccasionOwnerId",
                table: "Occasions",
                column: "OwnerOccasionOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OccasionOwner_OwnerId",
                table: "OccasionOwner",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occasions_OccasionOwner_OwnerOccasionOwnerId",
                table: "Occasions",
                column: "OwnerOccasionOwnerId",
                principalTable: "OccasionOwner",
                principalColumn: "OccasionOwnerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occasions_OccasionOwner_OwnerOccasionOwnerId",
                table: "Occasions");

            migrationBuilder.DropTable(
                name: "OccasionOwner");

            migrationBuilder.DropIndex(
                name: "IX_Occasions_OwnerOccasionOwnerId",
                table: "Occasions");

            migrationBuilder.DropColumn(
                name: "OwnerOccasionOwnerId",
                table: "Occasions");
        }
    }
}
