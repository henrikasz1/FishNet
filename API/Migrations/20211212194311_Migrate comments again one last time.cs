using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Migratecommentsagainonelasttime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ShopAdverts_ShopId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ShopId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "Comments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ShopId",
                table: "Comments",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ShopAdverts_ShopId",
                table: "Comments",
                column: "ShopId",
                principalTable: "ShopAdverts",
                principalColumn: "ShopId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
