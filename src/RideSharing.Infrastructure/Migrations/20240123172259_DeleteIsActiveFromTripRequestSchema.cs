using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIsActiveFromTripRequestSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TripRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TripRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
