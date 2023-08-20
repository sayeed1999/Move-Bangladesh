namespace RideSharing.Entity
{
    // This schema is the customer ratings submitted by drivers
    public class CustomerRating : Rating
    {
        private CustomerRating() : base() { }
        private CustomerRating(long customerId, Customer customer, long driverId, Driver driver, long tripId, Trip trip, short ratingValue, string feedback)
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

        public static CustomerRating Create(long customerId, Customer customer, long driverId, Driver driver, long tripId, Trip trip, short ratingValue, string feedback)
        {
            var customerRating = new CustomerRating();
            return customerRating;
        }
    }
}