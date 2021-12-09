using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Occasions2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occasions_OccasionOwner_OwnerOccasionOwnerId",
                table: "Occasions");

            migrationBuilder.DropIndex(
                name: "IX_Occasions_OwnerOccasionOwnerId",
                table: "Occasions");

            migrationBuilder.DropColumn(
                name: "OwnerOccasionOwnerId",
                table: "Occasions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Occasions",
                newName: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_OwnerId",
                table: "Occasions",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occasions_OccasionOwner_OwnerId",
                table: "Occasions",
                column: "OwnerId",
                principalTable: "OccasionOwner",
                principalColumn: "OccasionOwnerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occasions_OccasionOwner_OwnerId",
                table: "Occasions");

            migrationBuilder.DropIndex(
                name: "IX_Occasions_OwnerId",
                table: "Occasions");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Occasions",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerOccasionOwnerId",
                table: "Occasions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_OwnerOccasionOwnerId",
                table: "Occasions",
                column: "OwnerOccasionOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occasions_OccasionOwner_OwnerOccasionOwnerId",
                table: "Occasions",
                column: "OwnerOccasionOwnerId",
                principalTable: "OccasionOwner",
                principalColumn: "OccasionOwnerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
