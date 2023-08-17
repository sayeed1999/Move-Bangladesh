using RideSharing.Common.Enums;

namespace RideSharing.Entity
{
    public class Customer : Human
    {
        private Customer() : base()
        {
            DriverRatings = new();
            Trips = new();
        }

        public virtual List<DriverRating> DriverRatings { get; private set; }
        public virtual List<Trip> Trips { get; private set; }

        public static Customer Create(long id, string firstName, string lastName, Gender gender, string email, string userName, string phoneNumber)
        {
            var customer = new Customer();
            return customer;
        }

    }
}