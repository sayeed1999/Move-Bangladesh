using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Persistence.EntityConfigurations
{
	internal class PaymentEntityConfiguration : IEntityTypeConfiguration<PaymentEntity>
	{
		public void Configure(EntityTypeBuilder<PaymentEntity> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.IsRequired()
				.HasMaxLength(30)
				.ValueGeneratedNever();

			builder
				.Property(x => x.TripId)
				.IsRequired()
				.HasMaxLength(30);


			builder
				.HasOne(x => x.Trip)
				.WithMany(x => x.Payments)
				.HasForeignKey(x => x.TripId);
		}
	}
}
