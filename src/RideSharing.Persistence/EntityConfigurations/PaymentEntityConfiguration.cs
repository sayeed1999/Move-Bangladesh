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
				.HasOne(x => x.Trip)
				.WithMany(x => x.Payments)
				.HasForeignKey(x => x.TripId);
		}
	}
}
