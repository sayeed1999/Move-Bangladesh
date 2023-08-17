using RideSharing.Entity.Enums;

namespace RideSharing.Entity
{
    public class Trip : Base
    {
        private Trip() : base() { }  
        private Trip(long customerId, Customer customer, long driverId, Driver driver, long paymentId, Payment payment, TripStatus status, string source, string destination) : base()
        {
            CustomerId = customerId;
            Customer = customer;
            DriverId = driverId;
            Driver = driver;
            PaymentId = paymentId;
            Payment = payment;
            Status = status;
            Source = source;
            Destination = destination;
        }

        public long CustomerId { get; protected set; }
        public virtual Customer Customer { get; protected set; }
        public long DriverId { get; protected set; }
        public virtual Driver Driver { get; protected set; }
        public long PaymentId { get; protected set; }
        public virtual Payment Payment { get; protected set; }
        public TripStatus Status { get; protected set; }
        public String Source { get; protected set; }
        public String Destination { get; protected set; }

        public static Trip Create(long customerId, Customer customer, long driverId, Driver driver, long paymentId, Payment payment, TripStatus status, string source, string destination)
        {
            var x = new Trip();
            return x;
        }
    }
}