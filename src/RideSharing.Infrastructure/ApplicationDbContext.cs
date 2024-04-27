using Microsoft.EntityFrameworkCore;
using RideSharing.Domain.Entities;
using RideSharing.Infrastructure.EntityConfigurations;

namespace RideSharing.Infrastructure;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	#region dbsets
	public DbSet<User> Users { get; set; }
	public DbSet<CustomerEntity> Customers { get; set; }
	public DbSet<CustomerRatingEntity> CustomerRatings { get; set; }
	public DbSet<DriverEntity> Drivers { get; set; }
	public DbSet<DriverRatingEntity> DriverRatings { get; set; }
	public DbSet<CabEntity> Cabs { get; set; }
	public DbSet<PaymentEntity> Payments { get; set; }
	public DbSet<TripRequestEntity> TripRequests { get; set; }
	public DbSet<TripRequestLogEntity> TripRequestLogs { get; set; }
	public DbSet<TripEntity> Trips { get; set; }
	public DbSet<TripLogEntity> TripLogs { get; set; }
	#endregion

	// Connection String is initialized from RideSharing.API -> Startup.cs...

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		// configure foreign keys
		builder.Entity<CabEntity>()
			.HasOne(x => x.Driver)
			.WithMany(x => x.Cabs)
			.HasForeignKey(x => x.DriverId);

		builder.Entity<CustomerRatingEntity>()
			.HasOne(x => x.Driver)
			.WithMany(x => x.CustomerRatings)
			.HasForeignKey(x => x.DriverId);

		// Note: Customer Rating Entity has also FK on Customer.CustomerId,
		// ef core giving error on putting that here but auto generated the FK!

		builder.Entity<DriverRatingEntity>()
			.HasOne(x => x.Customer)
			.WithMany(x => x.DriverRatings)
			.HasForeignKey(x => x.CustomerId);

		// Note: Driver Rating Entity has also FK on Driver.DriverId,
		// ef core giving error on putting that here but auto generated the FK!

		// Note:- we cannot have both TripId on Payments and PaymentId on Trips.
		// so we have to decide which should be the parent.
		// we choose TripId as FK on payment table
		// but we are not taking PaymentId as FK on trip table
		builder.Entity<TripEntity>()
			.HasOne(x => x.Payment)
			.WithOne(x => x.Trip)
			.HasForeignKey<PaymentEntity>(x => x.TripId);

		builder.Entity<TripLogEntity>()
			.HasOne(x => x.Trip)
			.WithMany(x => x.TripLogs)
			.HasForeignKey(x => x.TripId);

		builder.Entity<TripRequestLogEntity>()
			.HasOne(x => x.TripRequest)
			.WithMany(x => x.TripRequestLogs)
			.HasForeignKey(x => x.TripRequestId);

		// TODO:- if postgres is not being used, this method should not get called. Try Open-Closed principle!
		// To make sure that the PostGIS extension is installed in your database:
		builder.HasPostgresExtension("postgis");

		// restrict all cascade delete
		foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
		{
			relationship.DeleteBehavior = DeleteBehavior.Restrict;
		}

		#region Apply Entity Configurations

		new TripRequestEntityConfigurations().Configure(builder.Entity<TripRequestEntity>());

		new TripEntityConfigurations().Configure(builder.Entity<TripEntity>());

		#endregion
	}
}