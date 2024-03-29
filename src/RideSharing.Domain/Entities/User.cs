using RideSharing.Common.Enums;

namespace RideSharing.Domain.Entities;

public class User
{
	public User()
	{
		Gender = Gender.Unknown;
		IsVerified = false;
	}

	public Guid Id { get; set; }
	public Guid? AuthUserId { get; set; } // if user came from another authentication provider!!
	public Gender Gender { get; set; }
	public DateTime DOB { get; set; }
	public string Name { get; set; }
	public string Address { get; set; }
	public string Phone { get; set; }
	public string Email { get; set; }
	public bool? IsVerified { get; set; }
}
