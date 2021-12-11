using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class OccasionPhoto18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OccasionUser_AspNetUsers_UserId1",
                table: "OccasionUser");

            migrationBuilder.DropForeignKey(
                name: "FK_OccasionUser_Occasions_OccasionId",
                table: "OccasionUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OccasionUser",
                table: "OccasionUser");

            migrationBuilder.RenameTable(
                name: "OccasionUser",
                newName: "OccasionUsers");

            migrationBuilder.RenameIndex(
                name: "IX_OccasionUser_UserId1",
                table: "OccasionUsers",
                newName: "IX_OccasionUsers_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_OccasionUser_OccasionId",
                table: "OccasionUsers",
                newName: "IX_OccasionUsers_OccasionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OccasionUsers",
                table: "OccasionUsers",
                columns: new[] { "UserId", "OccasionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OccasionUsers_AspNetUsers_UserId1",
                table: "OccasionUsers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OccasionUsers_Occasions_OccasionId",
                table: "OccasionUsers",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "OccasionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OccasionUsers_AspNetUsers_UserId1",
                table: "OccasionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OccasionUsers_Occasions_OccasionId",
                table: "OccasionUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OccasionUsers",
                table: "OccasionUsers");

            migrationBuilder.RenameTable(
                name: "OccasionUsers",
                newName: "OccasionUser");

            migrationBuilder.RenameIndex(
                name: "IX_OccasionUsers_UserId1",
                table: "OccasionUser",
                newName: "IX_OccasionUser_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_OccasionUsers_OccasionId",
                table: "OccasionUser",
                newName: "IX_OccasionUser_OccasionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OccasionUser",
                table: "OccasionUser",
                columns: new[] { "UserId", "OccasionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OccasionUser_AspNetUsers_UserId1",
                table: "OccasionUser",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OccasionUser_Occasions_OccasionId",
                table: "OccasionUser",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "OccasionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
