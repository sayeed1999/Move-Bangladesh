using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCabTypeToTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CabType",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CabType",
                table: "Trips");
        }
    }
}
