﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideSharing.Domain.Entities;

namespace RideSharing.Persistence.EntityConfigurations
{
	internal class DriverRatingEntityConfiguration : IEntityTypeConfiguration<DriverRatingEntity>
	{
		public void Configure(EntityTypeBuilder<DriverRatingEntity> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.IsRequired()
				.HasMaxLength(30)
				.ValueGeneratedNever();

			builder
				.Property(x => x.CustomerId)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.DriverId)
				.IsRequired()
				.HasMaxLength(30);

			builder
				.Property(x => x.TripId)
				.IsRequired()
				.HasMaxLength(30);


			builder
				.HasOne(x => x.Driver)
				.WithMany(x => x.DriverRatings)
				.HasForeignKey(x => x.DriverId);

			builder
				.HasOne(x => x.Customer)
				.WithMany(x => x.DriverRatings)
				.HasForeignKey(x => x.CustomerId);
		}
	}
}
