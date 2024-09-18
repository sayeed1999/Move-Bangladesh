using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Persistence.EntityConfigurations
{
	internal class CustomerRatingConfiguration : IEntityTypeConfiguration<CustomerRating>
	{
		public void Configure(EntityTypeBuilder<CustomerRating> builder)
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
