namespace RideSharing.Processor.TransitionChecker
{
	public interface ITransitionChecker<T>
	{
		bool IsTransitionValid(T fromStatus, T toStatus);
	}
}
