using Microsoft.EntityFrameworkCore.Migrations;

namespace TramWars.Migrations
{
    public partial class AddTargetStopToRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TargetLat",
                table: "Routes",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TargetLng",
                table: "Routes",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "TargetStopName",
                table: "Routes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetLat",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TargetLng",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TargetStopName",
                table: "Routes");
        }
    }
}
