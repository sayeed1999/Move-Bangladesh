namespace MoveBangladesh.Application.Abstractions
{
	public interface IUserContext
	{
		bool IsAuthenticated { get; }
		string UserId { get; }
	}
}
