namespace RideSharing.Application.Abstractions
{
	public interface IUserContext
	{
		bool IsAuthenticated { get; }
		long UserId { get; }
	}
}
