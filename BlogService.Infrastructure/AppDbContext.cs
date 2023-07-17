using BlogService.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region dbsets

        public DbSet<User> Users { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Edge> Edges { get; set; }

        #endregion dbsets

        // Connection String is initialized from BlogService.API -> Startup.cs...

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