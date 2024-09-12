using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Persistence.EntityConfigurations
{
	internal class CabEntityConfiguration : IEntityTypeConfiguration<CabEntity>
	{
		public void Configure(EntityTypeBuilder<CabEntity> builder)
		{
			builder
				.HasOne(x => x.Driver)
				.WithMany(x => x.Cabs)
				.HasForeignKey(x => x.DriverId);
		}
	}
}
