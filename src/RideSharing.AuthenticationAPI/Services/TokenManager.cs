using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RideSharing.AuthenticationAPI.Models;
using RideSharing.Common.Configurations;
using RideSharing.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RideSharing.AuthenticationAPI.Services;

public class TokenService(
	IOptions<Jwt> jwtOptions,
	UserManager<User> userManager)
{
	public async Task<TokenResponse> GenerateJwtTokenAsync(User user)
	{
		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

            // Adding additional user properties
            new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Name, user.UserName ?? ""),
			new Claim(ClaimTypes.Email, user.Email ?? ""),
			new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? "")
		};

		var userClaims = await userManager.GetClaimsAsync(user);
		var userRoles = await userManager.GetRolesAsync(user);
		var roleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToArray();

		claims = claims.Concat(userClaims).Concat(roleClaims).ToArray();

		var jwtConfig = jwtOptions.Value;

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var accessTokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtConfig.ExpireMinutes)),
			SigningCredentials = creds,
			Issuer = jwtConfig.Issuer,
			Audience = jwtConfig.Audience,
		};

		var refreshTokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddDays(1),
			SigningCredentials = creds,
			Issuer = jwtConfig.Issuer,
			Audience = jwtConfig.Audience,
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);
		var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

		return new TokenResponse
		{
			TokenType = "Bearer",
			AccessToken = tokenHandler.WriteToken(accessToken),
			ExpiresIn = jwtConfig.ExpireMinutes * 60,
			RefreshToken = tokenHandler.WriteToken(refreshToken),
		};
	}
}
