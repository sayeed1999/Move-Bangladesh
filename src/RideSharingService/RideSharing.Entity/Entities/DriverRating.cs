namespace RideSharing.Entity
{
    // This schema is the driver ratings submitted by customers
    public class DriverRating : Rating
    {
        private DriverRating() : base() { }
        private DriverRating(long customerId, Customer customer, long driverId, Driver driver, long tripId, Trip trip, short ratingValue, string feedback)
        {
            CustomerId = customerId;
            Customer = customer;
            DriverId = driverId;
            Driver = driver;
            TripId = tripId;
            Trip = trip;
            RatingValue = ratingValue;
            Feedback = feedback;
        }

        public static DriverRating Create(long customerId, Customer customer, long driverId, Driver driver, long tripId, Trip trip, short ratingValue, string feedback)
        {
            var x = new DriverRating();
            return x;
        }
    }
}