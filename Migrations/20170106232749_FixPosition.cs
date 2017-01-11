using Microsoft.EntityFrameworkCore.Migrations;

namespace TramWars.Migrations
{
    public partial class FixPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Lat",
                table: "Position",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Lng",
                table: "Position",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Position");
        }
    }
}
