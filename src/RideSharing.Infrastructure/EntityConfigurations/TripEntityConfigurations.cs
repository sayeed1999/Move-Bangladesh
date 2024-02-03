using Microsoft.EntityFrameworkCore;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.EntityConfigurations
{
	internal class TripEntityConfigurations
	{
		public void Configure(ModelBuilder builder)
		{
			builder.Entity<Trip>()
			.HasOne(x => x.Payment)
			.WithOne(x => x.Trip)
			.HasPrincipalKey<Payment>(x => x.TripId);

			builder.Entity<Trip>()
				.Property(x => x.Source)
				.HasColumnType("geometry (point)");

			builder.Entity<Trip>()
				.Property(x => x.Destination)
				.HasColumnType("geometry (point)");
		}
	}
}
