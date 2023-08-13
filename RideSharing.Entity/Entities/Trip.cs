using RideSharing.Entity.Enums;

namespace RideSharing.Entity
{
    public class Trip : Base
    {
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public long DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public long PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public TripStatus Status { get; set; }
        public String Source { get; set; }
        public String Destination { get; set; }
    }
}