namespace RideSharing.Domain.Entities;

public class CustomerEntity : Human
{
	public CustomerEntity()
	{
		DriverRatings = new HashSet<DriverRatingEntity>();
		Trips = new HashSet<TripEntity>();
	}

	public virtual ICollection<DriverRatingEntity> DriverRatings { get; private set; }
	public virtual ICollection<TripEntity> Trips { get; private set; }
}
