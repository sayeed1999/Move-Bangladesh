namespace RideSharing.Domain.Enums
{
	public enum CabType
	{
		TwoWheels,
		ThreeWheels,
		FourWheels,
		BigWheels,
	}

	public enum TripRequestStatus
	{
		NoDriverAccepted = 1,
		CustomerCanceled = 2,
		DriverAccepted = 3,
		DriverCanceled = 4,
	}

	public enum TripStatus
	{
		TripStarted = 1,
		TripCompleted = 2,
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
}