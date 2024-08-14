using System;

namespace RideSharing.AuthenticationAPI.Models;

public class RegisterModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
}
