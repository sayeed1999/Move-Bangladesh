using System;

namespace RideSharing.AuthenticationAPI.Models;

public class TokenResponse
{
	public string TokenType { get; set; }
	public string AccessToken { get; set; }
	public int ExpiresIn { get; set; }
	public string RefreshToken { get; set; }
}
