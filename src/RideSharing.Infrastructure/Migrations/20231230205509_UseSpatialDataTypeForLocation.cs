using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace RideSharing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseSpatialDataTypeForLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AlterColumn<Point>(
                name: "Source",
                table: "Trips",
                type: "geometry (point)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Point>(
                name: "Destination",
                table: "Trips",
                type: "geometry (point)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Trips",
                type: "text",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry (point)");

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Trips",
                type: "text",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry (point)");
        }
    }
}
