namespace RideSharing.Domain.Entities;

public class CustomerEntity : AppUser
{
	public CustomerEntity()
	{
		CustomerRatings = new HashSet<CustomerRatingEntity>();
		DriverRatings = new HashSet<DriverRatingEntity>();
		Trips = new HashSet<TripEntity>();
	}

	public string? HomeAddress { get; set; }
	public string? WorkAddress { get; set; }
	public virtual ICollection<CustomerRatingEntity> CustomerRatings { get; private set; }
	public virtual ICollection<DriverRatingEntity> DriverRatings { get; private set; }
	public virtual ICollection<TripEntity> Trips { get; private set; }
}
