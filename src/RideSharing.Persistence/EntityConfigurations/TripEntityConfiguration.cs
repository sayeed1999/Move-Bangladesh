using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Persistence.EntityConfigurations
{
	internal class TripConfiguration : IEntityTypeConfiguration<Trip>
	{
		public void Configure(EntityTypeBuilder<Trip> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.IsRequired()
				.HasMaxLength(30)
				.ValueGeneratedNever();

			builder
				.Property(x => x.CustomerId)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.DriverId)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.SourceX)
				.IsRequired();

			builder
				.Property(x => x.SourceY)
				.IsRequired();

			builder
				.Property(x => x.DestinationX)
				.IsRequired();

			builder
				.Property(x => x.DestinationY)
				.IsRequired();

			builder
				.HasMany(x => x.TripLogs)
				.WithOne(x => x.Trip)
				.HasForeignKey(x => x.TripId);

		}
	}
}
