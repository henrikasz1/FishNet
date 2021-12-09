using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Occasions6 : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Occasions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "HostId",
                table: "Occasions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_UserId",
                table: "Occasions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occasions_AspNetUsers_UserId",
                table: "Occasions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occasions_AspNetUsers_UserId",
                table: "Occasions");

            migrationBuilder.DropIndex(
                name: "IX_Occasions_UserId",
                table: "Occasions");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "Occasions");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Occasions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

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
