using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Persistence.EntityConfigurations
{
	internal class DriverConfiguration : IEntityTypeConfiguration<Driver>
	{
		public void Configure(EntityTypeBuilder<Driver> builder)
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
