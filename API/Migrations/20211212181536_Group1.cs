using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Group1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPhotos_Groups_GroupId",
                table: "GroupPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPhotos",
                table: "GroupPhotos");

            migrationBuilder.DropIndex(
                name: "IX_GroupPhotos_GroupId",
                table: "GroupPhotos");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "GroupPhotos");

            migrationBuilder.RenameTable(
                name: "GroupPhotos",
                newName: "GroupPhoto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPhoto",
                table: "GroupPhoto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPhoto_GroupId",
                table: "GroupPhoto",
                column: "GroupId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPhoto_Groups_GroupId",
                table: "GroupPhoto",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPhoto_Groups_GroupId",
                table: "GroupPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPhoto",
                table: "GroupPhoto");

            migrationBuilder.DropIndex(
                name: "IX_GroupPhoto_GroupId",
                table: "GroupPhoto");

            migrationBuilder.RenameTable(
                name: "GroupPhoto",
                newName: "GroupPhotos");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "GroupPhotos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPhotos",
                table: "GroupPhotos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPhotos_GroupId",
                table: "GroupPhotos",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPhotos_Groups_GroupId",
                table: "GroupPhotos",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
