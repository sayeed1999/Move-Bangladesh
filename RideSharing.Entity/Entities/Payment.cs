using RideSharing.Entity.Enums;

namespace RideSharing.Entity
{
    public class Payment : Base
    {
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; }
        public long Amount { get; private set; }
    }
}