using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorAuditProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "Trips",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "TripRequests",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "TripRequestLogs",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "TripLogs",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "Payments",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "Cabs",
                newName: "LastModifiedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Drivers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Drivers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Drivers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DriverRatings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DriverRatings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "DriverRatings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CustomerRatings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CustomerRatings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "CustomerRatings",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DriverRatings");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DriverRatings");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "DriverRatings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CustomerRatings");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CustomerRatings");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "CustomerRatings");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Trips",
                newName: "LastUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "TripRequests",
                newName: "LastUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "TripRequestLogs",
                newName: "LastUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "TripLogs",
                newName: "LastUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Payments",
                newName: "LastUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Cabs",
                newName: "LastUpdatedAt");
        }
    }
}
