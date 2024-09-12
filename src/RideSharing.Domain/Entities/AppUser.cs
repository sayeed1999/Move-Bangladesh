namespace RideSharing.Domain.Entities;

public abstract class AppUser : BaseEntity
{
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;

	// Note: - same user can have customer profile verified, but driver profile not!
	public bool IsVerified { get; set; }
}
