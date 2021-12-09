using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Occasions4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OccasionOwner_AspNetUsers_OwnerId",
                table: "OccasionOwner");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "OccasionOwner",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_OccasionOwner_OwnerId",
                table: "OccasionOwner",
                newName: "IX_OccasionOwner_UserId1");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "OccasionOwner",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_OccasionOwner_AspNetUsers_UserId1",
                table: "OccasionOwner",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OccasionOwner_AspNetUsers_UserId1",
                table: "OccasionOwner");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OccasionOwner");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "OccasionOwner",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_OccasionOwner_UserId1",
                table: "OccasionOwner",
                newName: "IX_OccasionOwner_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OccasionOwner_AspNetUsers_OwnerId",
                table: "OccasionOwner",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
