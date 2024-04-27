using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.EntityConfigurations
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

			// Here, we choose Trip table as the Principal & Payment table as the Dependent.
			// so we cannot set the config from Payment entity, but only trip entity.
			builder
				.HasOne(x => x.Payment)
				.WithOne(x => x.Trip)
				.HasForeignKey<PaymentEntity>(x => x.TripId);


			builder
				.HasMany(x => x.TripLogs)
				.WithOne(x => x.Trip)
				.HasForeignKey(x => x.TripId);

		}
	}
}
