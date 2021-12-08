using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RenameEventTableToOccasions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_UserId1",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Occasions");

            migrationBuilder.RenameIndex(
                name: "IX_Event_UserId1",
                table: "Occasions",
                newName: "IX_Occasions_UserId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Occasions",
                table: "Occasions",
                column: "OccasionId");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Occasions",
                table: "Occasions");

            migrationBuilder.RenameTable(
                name: "Occasions",
                newName: "Event");

            migrationBuilder.RenameIndex(
                name: "IX_Occasions_UserId1",
                table: "Event",
                newName: "IX_Event_UserId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "OccasionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_UserId1",
                table: "Event",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
