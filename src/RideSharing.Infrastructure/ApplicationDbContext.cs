﻿using Microsoft.EntityFrameworkCore;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	#region dbsets
	public DbSet<User> Users { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<CustomerRating> CustomerRatings { get; set; }
	public DbSet<Driver> Drivers { get; set; }
	public DbSet<DriverRating> DriverRatings { get; set; }
	public DbSet<Cab> Cabs { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<TripRequest> TripRequests { get; set; }
	public DbSet<Trip> Trips { get; set; }
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

		#region Trip Entity Properties

		builder.Entity<Trip>()
			.HasOne(x => x.Payment)
			.WithOne(x => x.Trip)
			.HasPrincipalKey<Payment>(x => x.TripId);

		builder.Entity<Trip>()
			.Property(x => x.Source)
			.HasColumnType("geometry (point)");

		builder.Entity<Trip>()
			.Property(x => x.Destination)
			.HasColumnType("geometry (point)");

		#endregion
	}
}