using Microsoft.AspNetCore.Http;
using RideSharing.Application.Abstractions;
using System.Security.Claims;

namespace RideSharing.Infrastructure.Repositories
{
	public sealed class UserContext(
		IHttpContextAccessor httpContextAccessor) : IUserContext
	{
		public bool IsAuthenticated =>
			httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated
			?? throw new Exception("User context is missing!");

		public Guid UserId =>
			httpContextAccessor.HttpContext?.User.GetUserId()
			?? throw new Exception("User context is missing!");
	}

	internal static class ClaimsPrincipalExtensions
	{
		public static Guid GetUserId(this ClaimsPrincipal? principal)
		{
			string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

			return Guid.TryParse(userId, out Guid parsedUserId) ?
				parsedUserId :
				throw new ApplicationException("User id is unavailable");
		}
	}
}
