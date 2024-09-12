namespace RideSharing.Domain.Entities;

public class Customer : User
{
	public Customer()
	{
		CustomerRatings = new HashSet<CustomerRating>();
		DriverRatings = new HashSet<DriverRating>();
		Trips = new HashSet<Trip>();
	}

	public string? HomeAddress { get; set; }
	public string? WorkAddress { get; set; }
	public virtual ICollection<CustomerRating> CustomerRatings { get; private set; }
	public virtual ICollection<DriverRating> DriverRatings { get; private set; }
	public virtual ICollection<Trip> Trips { get; private set; }
}
