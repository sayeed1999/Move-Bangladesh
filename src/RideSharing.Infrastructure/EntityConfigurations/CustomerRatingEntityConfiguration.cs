using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.EntityConfigurations
{
	internal class CustomerRatingEntityConfiguration : IEntityTypeConfiguration<CustomerRatingEntity>
	{
		public void Configure(EntityTypeBuilder<CustomerRatingEntity> builder)
		{
			builder
				.HasOne(x => x.Driver)
				.WithMany(x => x.CustomerRatings)
				.HasForeignKey(x => x.DriverId);

			// Note: Customer Rating Entity has also FK on Customer.CustomerId,
			// ef core giving error on putting that here but auto generated the FK!
		}
	}
}
