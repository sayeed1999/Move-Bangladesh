using RideSharing.Common.Enums;

namespace RideSharing.Domain.Entities;

public abstract class User : BaseEntity
{
	public required string Name { get; set; }
	public required string Email { get; set; }
	public required string Phone { get; set; }
	public string? Location { get; set; }
	public Gender? Gender { get; set; }
	public DateTime? DOB { get; set; }
	public bool IsVerified { get; set; }
}
