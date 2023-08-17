using RideSharing.Common.Enums;

namespace RideSharing.Entity
{
    public class Customer : Human
    {
        private Customer(long id, string firstName, string lastName, Gender gender, string email, string userName, string phoneNumber) : base()
        {
            Id = id;
            Name = firstName + " " + lastName;
            Gender = gender;
            Email = email;
            Phone = phoneNumber;
            
            DriverRatings = new();
            Trips = new();
        }
        

        public virtual List<DriverRating> DriverRatings { get; protected set; }
        public virtual List<Trip> Trips { get; protected set; }

        public static Customer Create(long id, string firstName, string lastName, Gender gender, string email, string userName, string phoneNumber)
        {
            var customer = new Customer(id, firstName, lastName, gender, email, userName, phoneNumber);
            return customer;
        }

    }
}