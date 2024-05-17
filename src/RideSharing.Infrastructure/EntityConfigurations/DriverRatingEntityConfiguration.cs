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
				.HasOne(x => x.Driver)
				.WithMany(x => x.DriverRatings)
				.HasForeignKey(x => x.DriverId);

			builder
				.HasOne(x => x.Customer)
				.WithMany(x => x.DriverRatings)
				.HasForeignKey(x => x.CustomerId);
		}
	}
}
