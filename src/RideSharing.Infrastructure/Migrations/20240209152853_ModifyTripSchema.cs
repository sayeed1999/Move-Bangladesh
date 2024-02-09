using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTripSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Payments_Id",
                table: "Trips");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Payments_TripId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TripRequestId",
                table: "Trips",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TripRequestId",
                table: "Trips",
                column: "TripRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TripId",
                table: "Payments",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Trips_TripId",
                table: "Payments",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_TripRequests_TripRequestId",
                table: "Trips",
                column: "TripRequestId",
                principalTable: "TripRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Trips_TripId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_TripRequests_TripRequestId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_TripRequestId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Payments_TripId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripRequestId",
                table: "Trips");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Payments_TripId",
                table: "Payments",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Payments_Id",
                table: "Trips",
                column: "Id",
                principalTable: "Payments",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
