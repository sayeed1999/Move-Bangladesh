using RideSharing.Common.Enums;

namespace RideSharing.Entity
{
    public class Driver : Human
    {
        private Driver() : base()
        {
            CustomerRatings = new();
            Trips = new();
        }
        public virtual List<CustomerRating> CustomerRatings { get; private set; }
        public virtual List<Trip> Trips { get; private set; }

        public static Driver Create(long id, string firstName, string lastName, Gender gender, string email, string userName, string phoneNumber)
        {
            var driver = new Driver();
            return driver;
        }
    }
}