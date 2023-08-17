namespace RideSharing.Entity
{
    // Since there are two schemas for rating e.g CustomerRating & DriverRating having similar field names,
    // so i inherit their props from a parent 'Rating' schema.
    public class Rating : Base
    {
        public long CustomerId { get; private set; }
        public virtual Customer Customer { get; private set; }
        public long DriverId { get; private set; }
        public virtual Driver Driver { get; private set; }
        public long TripId { get; private set; }
        public virtual Trip Trip { get; private set; }
        public short RatingValue { get; private set; }
        public string Feedback { get; private set; }
    }
}