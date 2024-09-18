using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Persistence.EntityConfigurations
{
	internal class DriverRatingConfiguration : IEntityTypeConfiguration<DriverRating>
	{
		public void Configure(EntityTypeBuilder<DriverRating> builder)
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
