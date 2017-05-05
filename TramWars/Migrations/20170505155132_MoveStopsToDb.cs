using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TramWars.Migrations
{
    public partial class MoveStopsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartLat",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "StartLng",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "StartStopName",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TargetLat",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TargetLng",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TargetStopName",
                table: "Routes");

            migrationBuilder.AddColumn<int>(
                name: "StartStopId",
                table: "Routes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetStopId",
                table: "Routes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    StopId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StartStopId",
                table: "Routes",
                column: "StartStopId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TargetStopId",
                table: "Routes",
                column: "TargetStopId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_StopId",
                table: "Service",
                column: "StopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Stops_StartStopId",
                table: "Routes",
                column: "StartStopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Stops_TargetStopId",
                table: "Routes",
                column: "TargetStopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stops_StartStopId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stops_TargetStopId",
                table: "Routes");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Routes_StartStopId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_TargetStopId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "StartStopId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TargetStopId",
                table: "Routes");

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
    }
}
