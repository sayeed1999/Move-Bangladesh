using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeUsertableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_User_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_User_UserId",
                table: "Drivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Trips",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Trips",
                newName: "DeletedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Trips",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Payments",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Payments",
                newName: "DeletedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Payments",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Drivers",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Drivers",
                newName: "DeletedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Drivers",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "DriverRatings",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "DriverRatings",
                newName: "DeletedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "DriverRatings",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Customers",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Customers",
                newName: "DeletedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Customers",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "CustomerRatings",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "CustomerRatings",
                newName: "DeletedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CustomerRatings",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Cabs",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Cabs",
                newName: "DeletedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Cabs",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AuthUserId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateUtc",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "DeletedById",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateUtc",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedById",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateUtc",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CreatedById",
                table: "Trips",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DeletedById",
                table: "Trips",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UpdatedById",
                table: "Trips",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CreatedById",
                table: "Payments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DeletedById",
                table: "Payments",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UpdatedById",
                table: "Payments",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CreatedById",
                table: "Drivers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_DeletedById",
                table: "Drivers",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_UpdatedById",
                table: "Drivers",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRatings_CreatedById",
                table: "DriverRatings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRatings_DeletedById",
                table: "DriverRatings",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRatings_UpdatedById",
                table: "DriverRatings",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedById",
                table: "Customers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DeletedById",
                table: "Customers",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UpdatedById",
                table: "Customers",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRatings_CreatedById",
                table: "CustomerRatings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRatings_DeletedById",
                table: "CustomerRatings",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRatings_UpdatedById",
                table: "CustomerRatings",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cabs_CreatedById",
                table: "Cabs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cabs_DeletedById",
                table: "Cabs",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cabs_UpdatedById",
                table: "Cabs",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthUserId",
                table: "Users",
                column: "AuthUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeletedById",
                table: "Users",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedById",
                table: "Users",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabs_Users_CreatedById",
                table: "Cabs",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cabs_Users_DeletedById",
                table: "Cabs",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cabs_Users_UpdatedById",
                table: "Cabs",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRatings_Users_CreatedById",
                table: "CustomerRatings",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRatings_Users_DeletedById",
                table: "CustomerRatings",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRatings_Users_UpdatedById",
                table: "CustomerRatings",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_CreatedById",
                table: "Customers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_DeletedById",
                table: "Customers",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UpdatedById",
                table: "Customers",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRatings_Users_CreatedById",
                table: "DriverRatings",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRatings_Users_DeletedById",
                table: "DriverRatings",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRatings_Users_UpdatedById",
                table: "DriverRatings",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Users_CreatedById",
                table: "Drivers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Users_DeletedById",
                table: "Drivers",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Users_UpdatedById",
                table: "Drivers",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Users_UserId",
                table: "Drivers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_CreatedById",
                table: "Payments",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_DeletedById",
                table: "Payments",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UpdatedById",
                table: "Payments",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_CreatedById",
                table: "Trips",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_DeletedById",
                table: "Trips",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_UpdatedById",
                table: "Trips",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_DeletedById",
                table: "Users",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UpdatedById",
                table: "Users",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabs_Users_CreatedById",
                table: "Cabs");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabs_Users_DeletedById",
                table: "Cabs");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabs_Users_UpdatedById",
                table: "Cabs");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRatings_Users_CreatedById",
                table: "CustomerRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRatings_Users_DeletedById",
                table: "CustomerRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRatings_Users_UpdatedById",
                table: "CustomerRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_CreatedById",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_DeletedById",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UpdatedById",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverRatings_Users_CreatedById",
                table: "DriverRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverRatings_Users_DeletedById",
                table: "DriverRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverRatings_Users_UpdatedById",
                table: "DriverRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Users_CreatedById",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Users_DeletedById",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Users_UpdatedById",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Users_UserId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_CreatedById",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_DeletedById",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_UpdatedById",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_CreatedById",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_DeletedById",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_UpdatedById",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_DeletedById",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UpdatedById",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Trips_CreatedById",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_DeletedById",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_UpdatedById",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CreatedById",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_DeletedById",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UpdatedById",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_CreatedById",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_DeletedById",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_UpdatedById",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_DriverRatings_CreatedById",
                table: "DriverRatings");

            migrationBuilder.DropIndex(
                name: "IX_DriverRatings_DeletedById",
                table: "DriverRatings");

            migrationBuilder.DropIndex(
                name: "IX_DriverRatings_UpdatedById",
                table: "DriverRatings");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CreatedById",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DeletedById",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UpdatedById",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerRatings_CreatedById",
                table: "CustomerRatings");

            migrationBuilder.DropIndex(
                name: "IX_CustomerRatings_DeletedById",
                table: "CustomerRatings");

            migrationBuilder.DropIndex(
                name: "IX_CustomerRatings_UpdatedById",
                table: "CustomerRatings");

            migrationBuilder.DropIndex(
                name: "IX_Cabs_CreatedById",
                table: "Cabs");

            migrationBuilder.DropIndex(
                name: "IX_Cabs_DeletedById",
                table: "Cabs");

            migrationBuilder.DropIndex(
                name: "IX_Cabs_UpdatedById",
                table: "Cabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AuthUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DeletedById",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UpdatedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AuthUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDateUtc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedDateUtc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedDateUtc",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Trips",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedById",
                table: "Trips",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Trips",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Payments",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedById",
                table: "Payments",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Payments",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Drivers",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedById",
                table: "Drivers",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Drivers",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "DriverRatings",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedById",
                table: "DriverRatings",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "DriverRatings",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Customers",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedById",
                table: "Customers",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Customers",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "CustomerRatings",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedById",
                table: "CustomerRatings",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "CustomerRatings",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Cabs",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "DeletedById",
                table: "Cabs",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Cabs",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "User",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "FirstName");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "User",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_User_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_User_UserId",
                table: "Drivers",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
