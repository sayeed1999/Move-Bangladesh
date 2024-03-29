using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Entities;

public class Cab : BaseEntity
{
	public string RegNo { get; set; }
	public Guid DriverId { get; set; }
	public virtual Driver Driver { get; set; }
	public CabType CabType { get; set; }
}
