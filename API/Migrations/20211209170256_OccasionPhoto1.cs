using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class OccasionPhoto1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "OccasionPhoto",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "OccasionPhoto");
        }
    }
}
