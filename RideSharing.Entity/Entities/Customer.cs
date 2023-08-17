namespace RideSharing.Entity
{
    public class Customer : Human
    {
        public virtual ICollection<DriverRating> DriverRatings { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}