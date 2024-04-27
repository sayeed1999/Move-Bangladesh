namespace RideSharing.Application.Abstractions
{
	public interface IUserContext
	{
		bool IsAuthenticated { get; }
		Guid UserId { get; }
	}
}
