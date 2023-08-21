using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AuthUserId",
                table: "Drivers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AuthUserId",
                table: "Customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthUserId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "AuthUserId",
                table: "Customers");
        }
    }
}
