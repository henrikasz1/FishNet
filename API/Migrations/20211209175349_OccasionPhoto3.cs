using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class OccasionPhoto3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OccasionPhoto_Occasions_OccasionId",
                table: "OccasionPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OccasionPhoto",
                table: "OccasionPhoto");

            migrationBuilder.RenameTable(
                name: "OccasionPhoto",
                newName: "OccasionsPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_OccasionPhoto_OccasionId",
                table: "OccasionsPhotos",
                newName: "IX_OccasionsPhotos_OccasionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OccasionsPhotos",
                table: "OccasionsPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OccasionsPhotos_Occasions_OccasionId",
                table: "OccasionsPhotos",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "OccasionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OccasionsPhotos_Occasions_OccasionId",
                table: "OccasionsPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OccasionsPhotos",
                table: "OccasionsPhotos");

            migrationBuilder.RenameTable(
                name: "OccasionsPhotos",
                newName: "OccasionPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_OccasionsPhotos_OccasionId",
                table: "OccasionPhoto",
                newName: "IX_OccasionPhoto_OccasionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OccasionPhoto",
                table: "OccasionPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OccasionPhoto_Occasions_OccasionId",
                table: "OccasionPhoto",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "OccasionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
