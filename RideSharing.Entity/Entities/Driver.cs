using RideSharing.Common.Enums;

namespace RideSharing.Entity
{
    public class Driver : Human
    {
        private Driver(long id, string firstName, string lastName, Gender gender, string email, string userName, string phoneNumber) : base()
        {
            Id = id;
            Name = firstName + " " + lastName;
            Gender = gender;
            Email = email;
            Phone = phoneNumber;

            CustomerRatings = new();
            Trips = new();
        }
        public virtual List<CustomerRating> CustomerRatings { get; protected set; }
        public virtual List<Trip> Trips { get; protected set; }

        public static Driver Create(long id, string firstName, string lastName, Gender gender, string email, string userName, string phoneNumber)
        {
            var driver = new Driver(id, firstName, lastName, gender, email, userName,phoneNumber);
            return driver;
        }
    }
}