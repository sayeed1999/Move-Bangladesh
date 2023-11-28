using RideSharing.Entity.Enums;
using Sayeed.Generic.OnionArchitecture.Entity;

namespace RideSharing.Entity
{
    public class Payment : BaseEntity
    {
        private Payment() :base() { }

        private Payment(PaymentMethod method, PaymentStatus status, long amount) : base()
        {
            Method = method;
            Status = status;
            Amount = amount;
        }

        public PaymentMethod Method { get; protected set; }
        public PaymentStatus Status { get; protected set; }
        public long Amount { get; protected set; }

        public static Payment Create(PaymentMethod Method, PaymentStatus Status, long Amount)
        {
            var payment = new Payment();
            return payment;
        }
    }
}