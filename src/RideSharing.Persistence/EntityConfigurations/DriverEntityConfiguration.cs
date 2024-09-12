using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Persistence.EntityConfigurations
{
	internal class DriverEntityConfiguration : IEntityTypeConfiguration<DriverEntity>
	{
		public void Configure(EntityTypeBuilder<DriverEntity> builder)
		{
			builder
				.HasIndex(x => x.Email)
				.IsUnique();

			builder
				.HasIndex(x => x.Phone)
				.IsUnique();
		}
	}
}
