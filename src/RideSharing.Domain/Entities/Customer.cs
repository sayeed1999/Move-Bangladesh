namespace RideSharing.Domain.Entities;

public class Customer : Human
{
	public Customer()
	{
		DriverRatings = new HashSet<DriverRating>();
		Trips = new HashSet<Trip>();
	}

	public virtual ICollection<DriverRating> DriverRatings { get; private set; }
	public virtual ICollection<Trip> Trips { get; private set; }
}
