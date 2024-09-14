using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RideSharing.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class InitialMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			#region identity tables 'AspNet' prefixed
			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<string>(type: "text", nullable: false),
					Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<string>(type: "text", nullable: false),
					Name = table.Column<string>(type: "text", nullable: false),
					Location = table.Column<string>(type: "text", nullable: true),
					Gender = table.Column<int>(type: "integer", nullable: true),
					DOB = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
					Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
					PasswordHash = table.Column<string>(type: "text", nullable: true),
					SecurityStamp = table.Column<string>(type: "text", nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
					PhoneNumber = table.Column<string>(type: "text", nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
					TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
					LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
					AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					RoleId = table.Column<string>(type: "text", nullable: false),
					ClaimType = table.Column<string>(type: "text", nullable: true),
					ClaimValue = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					UserId = table.Column<string>(type: "text", nullable: false),
					ClaimType = table.Column<string>(type: "text", nullable: true),
					ClaimValue = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetUserClaims_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(type: "text", nullable: false),
					ProviderKey = table.Column<string>(type: "text", nullable: false),
					ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
					UserId = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_AspNetUserLogins_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserId = table.Column<string>(type: "text", nullable: false),
					RoleId = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserId = table.Column<string>(type: "text", nullable: false),
					LoginProvider = table.Column<string>(type: "text", nullable: false),
					Name = table.Column<string>(type: "text", nullable: false),
					Value = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_AspNetUserTokens_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});
			#endregion

			// Note: - Only two tables, Customers & Drivers are the bridge to 08 AspNetTables

			// Customer has FK to AspNetUser
			migrationBuilder.CreateTable(
				name: "Customers",
				columns: table => new
				{
					Id = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					UserId = table.Column<string>(type: "text", nullable: false),
					HomeAddress = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
					WorkAddress = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
					CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					Email = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					PhoneNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					IsVerified = table.Column<bool>(type: "boolean", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Customers", x => x.Id);
					table.ForeignKey(
						name: "FK_Customers_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			// Driver table has FK to AspNetUser
			migrationBuilder.CreateTable(
				name: "Drivers",
				columns: table => new
				{
					Id = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					UserId = table.Column<string>(type: "text", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					Email = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					PhoneNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					IsVerified = table.Column<bool>(type: "boolean", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Drivers", x => x.Id);
					table.ForeignKey(
						name: "FK_Drivers_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Cabs",
				columns: table => new
				{
					Id = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					RegNo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					DriverId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					CabType = table.Column<int>(type: "integer", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Cabs", x => x.Id);
					table.ForeignKey(
						name: "FK_Cabs_Drivers_DriverId",
						column: x => x.DriverId,
						principalTable: "Drivers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "TripRequests",
				columns: table => new
				{
					Id = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					CustomerId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					DriverId = table.Column<string>(type: "character varying(30)", nullable: true),
					SourceX = table.Column<float>(type: "real", nullable: false),
					SourceY = table.Column<float>(type: "real", nullable: false),
					DestinationX = table.Column<float>(type: "real", nullable: false),
					DestinationY = table.Column<float>(type: "real", nullable: false),
					CabType = table.Column<int>(type: "integer", nullable: false),
					PaymentMethod = table.Column<int>(type: "integer", nullable: false),
					Status = table.Column<int>(type: "integer", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TripRequests", x => x.Id);
					table.ForeignKey(
						name: "FK_TripRequests_Customers_CustomerId",
						column: x => x.CustomerId,
						principalTable: "Customers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_TripRequests_Drivers_DriverId",
						column: x => x.DriverId,
						principalTable: "Drivers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Trips",
				columns: table => new
				{
					Id = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					TripRequestId = table.Column<string>(type: "character varying(30)", nullable: false),
					CustomerId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					DriverId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					PaymentMethod = table.Column<int>(type: "integer", nullable: false),
					TripStatus = table.Column<int>(type: "integer", nullable: false),
					SourceX = table.Column<float>(type: "real", nullable: false),
					SourceY = table.Column<float>(type: "real", nullable: false),
					DestinationX = table.Column<float>(type: "real", nullable: false),
					DestinationY = table.Column<float>(type: "real", nullable: false),
					CabType = table.Column<int>(type: "integer", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Trips", x => x.Id);
					table.ForeignKey(
						name: "FK_Trips_Customers_CustomerId",
						column: x => x.CustomerId,
						principalTable: "Customers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Trips_Drivers_DriverId",
						column: x => x.DriverId,
						principalTable: "Drivers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Trips_TripRequests_TripRequestId",
						column: x => x.TripRequestId,
						principalTable: "TripRequests",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "CustomerRatings",
				columns: table => new
				{
					Id = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					CustomerId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					DriverId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					TripId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					RatingValue = table.Column<short>(type: "smallint", nullable: false),
					Feedback = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CustomerRatings", x => x.Id);
					table.ForeignKey(
						name: "FK_CustomerRatings_Customers_CustomerId",
						column: x => x.CustomerId,
						principalTable: "Customers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_CustomerRatings_Drivers_DriverId",
						column: x => x.DriverId,
						principalTable: "Drivers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_CustomerRatings_Trips_TripId",
						column: x => x.TripId,
						principalTable: "Trips",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "DriverRatings",
				columns: table => new
				{
					Id = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					CustomerId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					DriverId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					TripId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					RatingValue = table.Column<short>(type: "smallint", nullable: false),
					Feedback = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DriverRatings", x => x.Id);
					table.ForeignKey(
						name: "FK_DriverRatings_Customers_CustomerId",
						column: x => x.CustomerId,
						principalTable: "Customers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_DriverRatings_Drivers_DriverId",
						column: x => x.DriverId,
						principalTable: "Drivers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_DriverRatings_Trips_TripId",
						column: x => x.TripId,
						principalTable: "Trips",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Payments",
				columns: table => new
				{
					Id = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					TripId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
					PaymentMethod = table.Column<int>(type: "integer", nullable: false),
					PaymentStatus = table.Column<int>(type: "integer", nullable: false),
					Amount = table.Column<int>(type: "integer", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Payments", x => x.Id);
					table.ForeignKey(
						name: "FK_Payments_Trips_TripId",
						column: x => x.TripId,
						principalTable: "Trips",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_AspNetRoleClaims_RoleId",
				table: "AspNetRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserId",
				table: "AspNetUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserLogins_UserId",
				table: "AspNetUserLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_RoleId",
				table: "AspNetUserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Cabs_DriverId",
				table: "Cabs",
				column: "DriverId");

			migrationBuilder.CreateIndex(
				name: "IX_CustomerRatings_CustomerId",
				table: "CustomerRatings",
				column: "CustomerId");

			migrationBuilder.CreateIndex(
				name: "IX_CustomerRatings_DriverId",
				table: "CustomerRatings",
				column: "DriverId");

			migrationBuilder.CreateIndex(
				name: "IX_CustomerRatings_TripId",
				table: "CustomerRatings",
				column: "TripId");

			migrationBuilder.CreateIndex(
				name: "IX_Customers_Email",
				table: "Customers",
				column: "Email",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Customers_PhoneNumber",
				table: "Customers",
				column: "PhoneNumber",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Customers_UserId",
				table: "Customers",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_DriverRatings_CustomerId",
				table: "DriverRatings",
				column: "CustomerId");

			migrationBuilder.CreateIndex(
				name: "IX_DriverRatings_DriverId",
				table: "DriverRatings",
				column: "DriverId");

			migrationBuilder.CreateIndex(
				name: "IX_DriverRatings_TripId",
				table: "DriverRatings",
				column: "TripId");

			migrationBuilder.CreateIndex(
				name: "IX_Drivers_Email",
				table: "Drivers",
				column: "Email",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Drivers_PhoneNumber",
				table: "Drivers",
				column: "PhoneNumber",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Drivers_UserId",
				table: "Drivers",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Payments_TripId",
				table: "Payments",
				column: "TripId");

			migrationBuilder.CreateIndex(
				name: "IX_TripRequests_CustomerId",
				table: "TripRequests",
				column: "CustomerId");

			migrationBuilder.CreateIndex(
				name: "IX_TripRequests_DriverId",
				table: "TripRequests",
				column: "DriverId");

			migrationBuilder.CreateIndex(
				name: "IX_Trips_CustomerId",
				table: "Trips",
				column: "CustomerId");

			migrationBuilder.CreateIndex(
				name: "IX_Trips_DriverId",
				table: "Trips",
				column: "DriverId");

			migrationBuilder.CreateIndex(
				name: "IX_Trips_TripRequestId",
				table: "Trips",
				column: "TripRequestId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserLogins");

			migrationBuilder.DropTable(
				name: "AspNetUserRoles");

			migrationBuilder.DropTable(
				name: "AspNetUserTokens");

			migrationBuilder.DropTable(
				name: "Cabs");

			migrationBuilder.DropTable(
				name: "CustomerRatings");

			migrationBuilder.DropTable(
				name: "DriverRatings");

			migrationBuilder.DropTable(
				name: "Payments");

			migrationBuilder.DropTable(
				name: "AspNetRoles");

			migrationBuilder.DropTable(
				name: "Trips");

			migrationBuilder.DropTable(
				name: "TripRequests");

			migrationBuilder.DropTable(
				name: "Customers");

			migrationBuilder.DropTable(
				name: "Drivers");

			migrationBuilder.DropTable(
				name: "AspNetUsers");
		}
	}
}
