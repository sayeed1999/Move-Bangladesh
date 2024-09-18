using Microsoft.AspNetCore.Identity;
using MoveBangladesh.Common.Enums;

namespace MoveBangladesh.Domain.Entities;

public class User : IdentityUser
{
	public string Name { get; set; }
	public string? Location { get; set; }
	public Gender? Gender { get; set; }
	public DateTime? DOB { get; set; }
}
