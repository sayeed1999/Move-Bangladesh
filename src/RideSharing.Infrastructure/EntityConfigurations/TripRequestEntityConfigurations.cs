using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.EntityConfigurations
{
	internal class TripRequestEntityConfigurations : IEntityTypeConfiguration<TripRequestEntity>
	{
		public void Configure(EntityTypeBuilder<TripRequestEntity> builder)
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
