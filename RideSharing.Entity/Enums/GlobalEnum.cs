namespace RideSharing.Entity.Enums
{
    public enum CabType
    {
        TwoWheels,
        ThreeWheels,
        FourWheels,
        BigWheels,
    }

    public enum TripStatus
    {
        TripRequested,
        TripAccepted,
        TripCanceled,
        JourneyStarted,
        JourneyEnded,
    }

    public enum PaymentMethod
    {
        Cash,
        Bkash,
        Nagad,
        Card,
    }

    public enum PaymentStatus
    {
        Processing,
        Failed,
        Success,
    }
}