using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Persistence.EntityConfigurations
{
	internal class TripRequestConfiguration : IEntityTypeConfiguration<TripRequest>
	{
		public void Configure(EntityTypeBuilder<TripRequest> builder)
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

			// builder
			// 	.HasMany(x => x.TripRequestLogs)
			// 	.WithOne(x => x.TripRequest)
			// 	.HasForeignKey(x => x.TripRequestId);
		}
	}
}
