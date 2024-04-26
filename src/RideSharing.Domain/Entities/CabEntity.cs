using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Entities;

public class CabEntity : BaseEntity
{
	public string RegNo { get; set; }
	public Guid DriverId { get; set; }
	public virtual DriverEntity Driver { get; set; }
	public CabType CabType { get; set; }
}
