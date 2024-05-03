namespace RideSharing.Processor
{
	public interface ITransitionChecker<T>
	{
		bool IsTransitionValid(T fromStatus, T toStatus);
	}
}
