namespace RideSharing.Domain.Entities;

public class DriverEntity : Human
{
	public DriverEntity()
	{
		CustomerRatings = new HashSet<CustomerRatingEntity>();
		Trips = new HashSet<TripEntity>();
	}

	public virtual ICollection<CustomerRatingEntity> CustomerRatings { get; private set; }
	public virtual ICollection<TripEntity> Trips { get; private set; }
}
