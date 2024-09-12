using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Persistence.EntityConfigurations
{
	internal class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
	{
		public void Configure(EntityTypeBuilder<CustomerEntity> builder)
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
				.Property(x => x.Phone)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.HomeAddress)
				.IsRequired(false)
				.HasMaxLength(60);

			builder
				.Property(x => x.WorkAddress)
				.IsRequired(false)
				.HasMaxLength(60);


			// create indexes

			builder
				.HasIndex(x => x.Email)
				.IsUnique();

			builder
				.HasIndex(x => x.Phone)
				.IsUnique();
		}
	}
}
