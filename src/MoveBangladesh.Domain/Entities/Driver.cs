namespace MoveBangladesh.Domain.Entities;

public class Driver : AppUser
{
	public Driver()
	{
		Cabs = new HashSet<Cab>();
		CustomerRatings = new HashSet<CustomerRating>();
		DriverRatings = new HashSet<DriverRating>();
		Trips = new HashSet<Trip>();
	}

	public string UserId { get; set; }
	public virtual User? User { get; set; }
	public virtual ICollection<Cab> Cabs { get; set; }
	public virtual ICollection<CustomerRating> CustomerRatings { get; private set; }
	public virtual ICollection<DriverRating> DriverRatings { get; private set; }
	public virtual ICollection<Trip> Trips { get; private set; }
}
