using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Persistence.EntityConfigurations
{
	internal class TripEntityConfiguration : IEntityTypeConfiguration<TripEntity>
	{
		public void Configure(EntityTypeBuilder<TripEntity> builder)
		{
			builder
				.Property(x => x.Source)
				.HasColumnType("geometry (point)");

			builder
				.Property(x => x.Destination)
				.HasColumnType("geometry (point)");

			builder
				.HasMany(x => x.TripLogs)
				.WithOne(x => x.Trip)
				.HasForeignKey(x => x.TripId);

		}
	}
}
