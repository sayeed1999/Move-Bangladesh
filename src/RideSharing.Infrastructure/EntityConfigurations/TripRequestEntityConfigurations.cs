using Microsoft.EntityFrameworkCore;
using RideSharing.Domain.Entities;

namespace RideSharing.Infrastructure.EntityConfigurations
{
	internal class TripRequestEntityConfigurations
	{
		public void Configure(ModelBuilder builder)
		{
			builder.Entity<TripRequest>()
				.Property(x => x.Source)
				.HasColumnType("geometry (point)");

			builder.Entity<TripRequest>()
				.Property(x => x.Destination)
				.HasColumnType("geometry (point)");
		}
	}
}
