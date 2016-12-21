using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TramWars.Persistence;

namespace TramWars.Migrations
{
    [DbContext(typeof(TramWarsContext))]
    [Migration("20161207204748_AddRoute")]
    partial class AddRoute
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

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

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("TramWars.Domain.Position", b =>
                {
                    b.HasOne("TramWars.Domain.Route")
                        .WithMany("Positions")
                        .HasForeignKey("RouteId");
                });
        }
    }
}
