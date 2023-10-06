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
        TripRequested = 0,
        CustomerCanceledBeforeAccepting = 1,
        TripAccepted = 2,
        CustomerCanceledAfterAccepting = 3,
        RiderCanceled = 4,
        JourneyStarted = 5,
        JourneyEnded = 6,
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