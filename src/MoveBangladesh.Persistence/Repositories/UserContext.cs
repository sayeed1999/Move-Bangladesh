using Microsoft.AspNetCore.Http;
using MoveBangladesh.Application.Abstractions;
using System.Security.Claims;

namespace MoveBangladesh.Persistence.Repositories
{
	public sealed class UserContext(
		IHttpContextAccessor httpContextAccessor) : IUserContext
	{
		public bool IsAuthenticated =>
			httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated
			?? throw new Exception("User context is missing!");

		public string UserId =>
			httpContextAccessor.HttpContext?.User.GetUserId()
			?? throw new Exception("User context is missing!");
	}

	internal static class ClaimsPrincipalExtensions
	{
		public static string GetUserId(this ClaimsPrincipal? principal)
		{
			string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

			return string.IsNullOrWhiteSpace(userId)
				? userId
				: throw new ApplicationException("User id is unavailable");
		}
	}
}
