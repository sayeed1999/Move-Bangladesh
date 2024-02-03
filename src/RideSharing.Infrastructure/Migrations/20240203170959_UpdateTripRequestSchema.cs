using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace RideSharing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTripRequestSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Source",
                table: "TripRequests",
                type: "geometry (point)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry");

            migrationBuilder.AlterColumn<Point>(
                name: "Destination",
                table: "TripRequests",
                type: "geometry (point)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Source",
                table: "TripRequests",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry (point)");

            migrationBuilder.AlterColumn<Point>(
                name: "Destination",
                table: "TripRequests",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry (point)");
        }
    }
}
