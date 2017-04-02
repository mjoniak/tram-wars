using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TramWars.Persistence;

namespace TramWars.Migrations
{
    [DbContext(typeof(TramWarsContext))]
    [Migration("20170402113543_RemoveAuth")]
    partial class RemoveAuth
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("TramWars.Domain.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Score");

                    b.HasKey("Id");

                    b.ToTable("AppUser");
                });

            modelBuilder.Entity("TramWars.Domain.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Lat");

                    b.Property<float>("Lng");

                    b.Property<int?>("RouteId");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("TramWars.Domain.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsClosed");

                    b.Property<float>("StartLat");

                    b.Property<float>("StartLng");

                    b.Property<string>("StartStopName");

                    b.Property<float>("TargetLat");

                    b.Property<float>("TargetLng");

                    b.Property<string>("TargetStopName");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("TramWars.Domain.Position", b =>
                {
                    b.HasOne("TramWars.Domain.Route")
                        .WithMany("Positions")
                        .HasForeignKey("RouteId");
                });

            modelBuilder.Entity("TramWars.Domain.Route", b =>
                {
                    b.HasOne("TramWars.Domain.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
