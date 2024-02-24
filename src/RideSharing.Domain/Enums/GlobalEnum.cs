namespace RideSharing.Domain.Enums
{
	public enum CabType
	{
		TwoWheels,
		ThreeWheels,
		FourWheels,
		BigWheels,
	}

	// Note: enum value a customer can only go to next stage, not before.
	public enum TripRequestStatus
	{
		NoDriverAccepted = 1, // finding driver
		CustomerCanceledBeforeDriverFound = 2, // lock a trip request once it reaches this stage
		DriverAccepted = 3, // driver may cancel, do not lock
		CustomerCanceledAfterDriverFound = 4, // lock a trip request once it reaches this stage
		DriverCanceled = 5, // lock a trip request once it reaches this stage
		TripStarted = 6, // lock a trip request once it reaches this stage
	}

	public enum TripStatus
	{
		DriverAccepted = 1,
		CustomerCanceled = 2,
		DriverCanceled = 3,
		TripStarted = 4,
		TripCompleted = 5,
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