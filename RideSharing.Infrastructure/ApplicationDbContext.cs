using Microsoft.EntityFrameworkCore;
using RideSharing.Entity;
using RideSharing.Entity.Entities;

namespace RideSharing.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region Rating

        public DbSet<CustomerRating> CustomerRatings { get; set; }
        public DbSet<DriverRating> DriverRatings { get; set; }

        #endregion Rating

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        #endregion User

        public DbSet<Cab> Cabs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public DbSet<RideRequest> RideRequests { get; set; }

        // Connection String is initialized from RideSharing.API -> Startup.cs...

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // restrict all cascade delete
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // overriding key behaviors..
            builder.Entity<User>()
                .HasIndex(e => e.AuthUserId)
                .IsUnique();

            builder.Entity<User>()
                .Ignore(e => e.CreatedBy)
                .Ignore(e => e.CreatedById);

            builder.Entity<User>()
                .HasOne(e => e.UpdatedBy)
                .WithMany()
                .HasForeignKey(e => e.UpdatedById);

            builder.Entity<User>()
                .HasOne(e => e.DeletedBy)
                .WithMany()
                .HasForeignKey(e => e.DeletedById);
        }
    }
}