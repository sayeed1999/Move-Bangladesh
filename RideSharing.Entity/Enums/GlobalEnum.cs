using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Entity.Enums
{
    public enum AccountStatus
    {
        Unknown,
        Active,
        Inactive,
    }

    public enum Gender
    {
        Unknown,
        Male,
        Female,
    }

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
