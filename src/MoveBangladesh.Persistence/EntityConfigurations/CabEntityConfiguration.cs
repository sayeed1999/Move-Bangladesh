using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Persistence.EntityConfigurations
{
	internal class CabConfiguration : IEntityTypeConfiguration<Cab>
	{
		public void Configure(EntityTypeBuilder<Cab> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.IsRequired()
				.HasMaxLength(30)
				.ValueGeneratedNever();

			builder
				.Property(x => x.RegNo)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.DriverId)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.HasOne(x => x.Driver)
				.WithMany(x => x.Cabs)
				.HasForeignKey(x => x.DriverId);
		}
	}
}
