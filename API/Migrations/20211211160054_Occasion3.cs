using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Occasion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantsCount",
                table: "OccasionUsers");

            migrationBuilder.AddColumn<int>(
                name: "ParticipantsCount",
                table: "Occasions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantsCount",
                table: "Occasions");

            migrationBuilder.AddColumn<int>(
                name: "ParticipantsCount",
                table: "OccasionUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
