using Sayeed.Generic.OnionArchitecture.Entity;

namespace RideSharing.Domain
{
    // Since there are two schemas for rating e.g CustomerRating & DriverRating having similar field names,
    // so i inherit their props from a parent 'Rating' schema.
    public abstract class Rating : BaseEntity
    {
        public Rating() : base()
        {

        }
        public long CustomerId { get; protected set; }
        public virtual Customer Customer { get; protected set; }
        public long DriverId { get; protected set; }
        public virtual Driver Driver { get; protected set; }
        public long TripId { get; protected set; }
        public virtual Trip Trip { get; protected set; }
        public short RatingValue { get; protected set; }
        public string Feedback { get; protected set; }
    }
}