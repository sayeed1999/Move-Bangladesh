using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.EntityConfigurations
{
	internal class TripEntityConfigurations : IEntityTypeConfiguration<TripEntity>
	{
		public void Configure(EntityTypeBuilder<TripEntity> builder)
		{
			builder
				.Property(x => x.Source)
				.HasColumnType("geometry (point)");

			builder
				.Property(x => x.Destination)
				.HasColumnType("geometry (point)");
		}
	}
}
