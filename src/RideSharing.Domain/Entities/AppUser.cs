using RideSharing.Common.Enums;

namespace RideSharing.Domain.Entities;

public abstract class AppUser
{
	public AppUser()
	{
		IsVerified = false;
	}

	public long Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string? Location { get; set; }
	public Gender? Gender { get; set; }
	public DateTime? DOB { get; set; }
	public bool IsVerified { get; set; }
}
