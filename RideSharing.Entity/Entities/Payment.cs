using RideSharing.Entity.Enums;

namespace RideSharing.Entity
{
    public class Payment : Base
    {
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public long Amount { get; set; }
    }
}