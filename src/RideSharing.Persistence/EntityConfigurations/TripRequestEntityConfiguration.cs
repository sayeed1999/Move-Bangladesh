using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Persistence.EntityConfigurations
{
	internal class TripRequestEntityConfiguration : IEntityTypeConfiguration<TripRequestEntity>
	{
		public void Configure(EntityTypeBuilder<TripRequestEntity> builder)
		{
			builder
				.Property(x => x.Source)
				.HasColumnType("geometry (point)");

			builder
				.Property(x => x.Destination)
				.HasColumnType("geometry (point)");

			builder
				.HasMany(x => x.TripRequestLogs)
				.WithOne(x => x.TripRequest)
				.HasForeignKey(x => x.TripRequestId);
		}
	}
}
