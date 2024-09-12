namespace RideSharing.Domain.Entities;

public class Driver : User
{
	public Driver()
	{
		Cabs = new HashSet<Cab>();
		CustomerRatings = new HashSet<CustomerRating>();
		DriverRatings = new HashSet<DriverRating>();
		Trips = new HashSet<Trip>();
	}

	public virtual ICollection<Cab> Cabs { get; set; }
	public virtual ICollection<CustomerRating> CustomerRatings { get; private set; }
	public virtual ICollection<DriverRating> DriverRatings { get; private set; }
	public virtual ICollection<Trip> Trips { get; private set; }
}
