using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Persistence.EntityConfigurations
{
	internal class CustomerRatingEntityConfiguration : IEntityTypeConfiguration<CustomerRatingEntity>
	{
		public void Configure(EntityTypeBuilder<CustomerRatingEntity> builder)
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
				.Property(x => x.TripId)
				.IsRequired()
				.HasMaxLength(30);

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
