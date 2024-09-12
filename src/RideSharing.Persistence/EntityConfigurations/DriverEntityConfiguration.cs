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
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.IsRequired()
				.HasMaxLength(30)
				.ValueGeneratedNever();

			builder
				.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.Email)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.PhoneNumber)
				.IsRequired()
				.HasMaxLength(30);


			// create indexes

			builder
				.HasIndex(x => x.Email)
				.IsUnique();

			builder
				.HasIndex(x => x.PhoneNumber)
				.IsUnique();
		}
	}
}
