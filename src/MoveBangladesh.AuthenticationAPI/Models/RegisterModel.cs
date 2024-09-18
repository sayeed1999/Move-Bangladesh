using System;

namespace MoveBangladesh.AuthenticationAPI.Models;

public class RegisterModel
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string PhoneNumber { get; set; }
	public string Role { get; set; }
}
