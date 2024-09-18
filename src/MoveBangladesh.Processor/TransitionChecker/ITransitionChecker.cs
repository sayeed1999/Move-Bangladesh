namespace MoveBangladesh.Processor.TransitionChecker
{
	public interface ITransitionChecker<T>
	{
		bool IsTransitionValid(T fromStatus, T toStatus);
	}
}
