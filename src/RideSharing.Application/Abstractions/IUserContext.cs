namespace RideSharing.Application.Abstractions
{
	public interface IUserContext
	{
		bool IsAuthenticated { get; }
		string UserId { get; }
	}
}
