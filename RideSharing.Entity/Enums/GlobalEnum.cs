namespace RideSharing.Entity.Enums
{
    public enum CabType
    {
        Unknown,
        TwoWheels,
        ThreeWheels,
        FourWheels,
        BigWheels,
    }

    public enum TripStatus
    {
        Unknown,
        TripRequested,
        TripAccepted,
        TripCanceled,
        JourneyStarted,
        JourneyEnded,
    }

    public enum PaymentMethod
    {
        Unknown,
        Cash,
        Bkash,
        Nagad,
        Card,
    }

    public enum PaymentStatus
    {
        Unknown,
        Processing,
        Failed,
        Success,
    }
}