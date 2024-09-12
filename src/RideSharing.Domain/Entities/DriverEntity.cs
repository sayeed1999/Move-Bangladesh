namespace RideSharing.Domain.Entities;

public class DriverEntity : User
{
	public DriverEntity()
	{
		Cabs = new HashSet<CabEntity>();
		CustomerRatings = new HashSet<CustomerRatingEntity>();
		DriverRatings = new HashSet<DriverRatingEntity>();
		Trips = new HashSet<TripEntity>();
	}

	public virtual ICollection<CabEntity> Cabs { get; set; }
	public virtual ICollection<CustomerRatingEntity> CustomerRatings { get; private set; }
	public virtual ICollection<DriverRatingEntity> DriverRatings { get; private set; }
	public virtual ICollection<TripEntity> Trips { get; private set; }
}
