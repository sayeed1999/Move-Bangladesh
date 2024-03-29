namespace RideSharing.Domain.Entities;

public class Driver : Human
{
	public Driver()
	{
		CustomerRatings = new HashSet<CustomerRating>();
		Trips = new HashSet<Trip>();
	}

	public virtual ICollection<CustomerRating> CustomerRatings { get; private set; }
	public virtual ICollection<Trip> Trips { get; private set; }
}
