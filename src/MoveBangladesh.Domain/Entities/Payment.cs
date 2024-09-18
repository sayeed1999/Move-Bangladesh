namespace MoveBangladesh.Domain.Entities;

public class Payment : BaseEntity
{
	public string TripId { get; set; } = string.Empty;
	public virtual Trip? Trip { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public PaymentStatus PaymentStatus { get; set; }
	public int Amount { get; set; }
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
	Processing = 1,
	Failed = 2,
	Success = 3,
}
