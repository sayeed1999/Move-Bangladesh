namespace RideSharing.ServiceBus.Abstractions
{
	public interface IEventBus
	{
		/// <summary>
		/// Use this to implement event bus publisher.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="integrationEvent"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task PublishAsync<T>(
			T integrationEvent,
			string queue = "",
			CancellationToken cancellationToken = default)
			where T : class;

		/// <summary>
		/// Use this to implement event bus subscriber.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="integrationEvent"></param>
		/// <param name="action"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task ConsumeAsync<T>(
			Func<T, Task> handleMessage,
			string queue = "",
			CancellationToken cancellationToken = default)
			where T : class;
	}
}
