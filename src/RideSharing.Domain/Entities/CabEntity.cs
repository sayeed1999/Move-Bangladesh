namespace RideSharing.Domain.Entities;

public class CabEntity : BaseEntity
{
	public string RegNo { get; set; }
	public long DriverId { get; set; }
	public virtual DriverEntity Driver { get; set; }
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
