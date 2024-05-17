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

		// TODO:- if postgres is not being used, this method should not get called. Try Open-Closed principle!
		// To make sure that the PostGIS extension is installed in your database:
		builder.HasPostgresExtension("postgis");

		// restrict all cascade delete
		foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
		{
			relationship.DeleteBehavior = DeleteBehavior.Restrict;
		}

		#region Apply Entity Configurations

		// Note:- In a 1-1 reln, we have to decide which should be the parent.
		// and the dependent one's FK will be auto set by EF Core.

		new CabEntityConfiguration().Configure(builder.Entity<CabEntity>());
		new CustomerEntityConfiguration().Configure(builder.Entity<CustomerEntity>());
		new DriverEntityConfiguration().Configure(builder.Entity<DriverEntity>());
		new CustomerRatingEntityConfiguration().Configure(builder.Entity<CustomerRatingEntity>());
		new DriverRatingEntityConfiguration().Configure(builder.Entity<DriverRatingEntity>());
		new PaymentEntityConfiguration().Configure(builder.Entity<PaymentEntity>());
		new TripEntityConfiguration().Configure(builder.Entity<TripEntity>());
		new TripRequestEntityConfiguration().Configure(builder.Entity<TripRequestEntity>());

		#endregion
	}
}