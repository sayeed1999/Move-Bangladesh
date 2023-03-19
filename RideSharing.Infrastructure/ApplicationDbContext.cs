
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RideSharing.Entity;
using System.Reflection.Emit;

namespace RideSharing.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region Rating
        public DbSet<CustomerRating> CustomerRatings { get; set; }
        public DbSet<DriverRating> DriverRatings { get; set; }
        #endregion

        #region User
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        #endregion

        public DbSet<Cab> Cabs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Trip> Trips { get; set; }

        // Connection String is initialized from RideSharing.API -> Startup.cs...

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // restrict all cascade delete
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Separate configuration files...

        }
    }
}