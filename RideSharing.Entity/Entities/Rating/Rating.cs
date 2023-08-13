namespace RideSharing.Entity
{
    // Since there are two schemas for rating e.g CustomerRating & DriverRating having similar field names,
    // so i inherit their props from a parent 'Rating' schema.
    public class Rating : Base
    {
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public long DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public long TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public short RatingValue { get; set; }
        public string Feedback { get; set; }
    }
}