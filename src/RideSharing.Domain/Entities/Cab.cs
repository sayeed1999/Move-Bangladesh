namespace RideSharing.Domain.Entities;

public class Cab : BaseEntity
{
	public required string RegNo { get; set; }
	public required string DriverId { get; set; }
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
