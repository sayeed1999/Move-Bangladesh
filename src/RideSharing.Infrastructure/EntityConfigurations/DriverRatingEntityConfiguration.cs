using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.EntityConfigurations
{
	internal class DriverRatingEntityConfiguration : IEntityTypeConfiguration<DriverRatingEntity>
	{
		public void Configure(EntityTypeBuilder<DriverRatingEntity> builder)
		{
			builder
				.HasOne(x => x.Customer)
				.WithMany(x => x.DriverRatings)
				.HasForeignKey(x => x.CustomerId);

			// Note: Driver Rating Entity has also FK on Driver.DriverId,
			// ef core giving error on putting that here but auto generated the FK!

		}
	}
}
