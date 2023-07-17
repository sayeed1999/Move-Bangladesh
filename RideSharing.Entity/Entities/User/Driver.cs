namespace RideSharing.Entity
{
    public class Driver : Human
    {
        public virtual ICollection<CustomerRating> CustomerRatings { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}