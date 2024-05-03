﻿namespace RideSharing.Domain.Entities;

public class PaymentEntity : BaseEntity
{
	public Guid TripId { get; set; }
	public virtual TripEntity Trip { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public PaymentStatus PaymentStatus { get; set; }
	public long Amount { get; set; }
}

public enum PaymentMethod
{
	CoD = 1, // Cash On Delivery
	Bkash = 2,
	Nagad = 3,
	Card = 4,
}

public enum PaymentStatus
{
	Processing,
	Failed,
	Success,
}
