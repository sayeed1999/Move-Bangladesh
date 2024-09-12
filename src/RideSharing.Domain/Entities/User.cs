using Microsoft.AspNetCore.Identity;
using RideSharing.Common.Enums;

namespace RideSharing.Domain.Entities;

public class User : IdentityUser
{
	public string Name { get; set; }
	public string? Location { get; set; }
	public Gender? Gender { get; set; }
	public DateTime? DOB { get; set; }
}
