using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.EntityConfigurations
{
	internal class CustomerRatingEntityConfiguration : IEntityTypeConfiguration<CustomerRatingEntity>
	{
		public void Configure(EntityTypeBuilder<CustomerRatingEntity> builder)
		{
			// TODO: investigate why ef core cant process this one!
			//builder
			//	.HasOne(x => x.Customer)
			//	.WithMany(x => x.CustomerRatings)
			//	.HasForeignKey(x => x.CustomerId);

			builder
				.HasOne(x => x.Driver)
				.WithMany(x => x.CustomerRatings)
				.HasForeignKey(x => x.DriverId);
		}
	}
}
