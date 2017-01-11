using Microsoft.EntityFrameworkCore.Migrations;

namespace TramWars.Migrations
{
    public partial class AddStartStopToRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "StartLat",
                table: "Routes",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StartLng",
                table: "Routes",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "StartStopName",
                table: "Routes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StartLat",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "StartLng",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "StartStopName",
                table: "Routes");
        }
    }
}
