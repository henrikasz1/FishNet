using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class ObjectIdFromGuidToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Likes",
                newName: "ObjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "ObjectId", "LoverId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "ObjectId",
                table: "Likes",
                newName: "PostId");
        }
    }
}
