namespace MoveBangladesh.Domain.Entities;

public class Cab : BaseEntity
{
	public Cab()
	{
	}

	public string RegNo { get; set; } = string.Empty;
	public string DriverId { get; set; } = string.Empty;
	public virtual Driver? Driver { get; set; }
	public CabType CabType { get; set; }
}

public enum CabType
{
	Rickshaw = 1,
	Bike = 1,
	Cng = 2,
	Car = 3,
	Microbus = 4,
}
