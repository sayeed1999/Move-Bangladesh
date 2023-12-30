using RideSharing.Common.Enums;

namespace RideSharing.Domain.Entities;

public class User
{
	public User()
	{
		Gender = Gender.Unknown;
		IsVerified = false;
	}

	public long Id { get; protected set; }
	public long? AuthUserId { get; protected set; } // if user came from another authentication provider!!
	public Gender Gender { get; protected set; }
	public DateTime DOB { get; protected set; }
	public string Name { get; protected set; }
	public string Address { get; protected set; }
	public string Phone { get; protected set; }
	public string Email { get; protected set; }
	public bool? IsVerified { get; protected set; }
}
