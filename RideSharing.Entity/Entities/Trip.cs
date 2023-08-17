using RideSharing.Entity.Enums;

namespace RideSharing.Entity
{
    public class Trip : Base
    {
        public long CustomerId { get; private set; }
        public virtual Customer Customer { get; private set; }
        public long DriverId { get; private set; }
        public virtual Driver Driver { get; private set; }
        public long PaymentId { get; private set; }
        public virtual Payment Payment { get; private set; }
        public TripStatus Status { get; private set; }
        public String Source { get; private set; }
        public String Destination { get; private set; }
    }
}