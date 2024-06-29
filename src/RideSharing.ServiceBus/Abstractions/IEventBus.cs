namespace RideSharing.ServiceBus.Abstractions
{
	public interface IEventBus
	{
		/// <summary>
		/// Publish a message
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="integrationEvent"></param>
		/// <param name="queue"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task PublishAsync<T>(
			T integrationEvent,
			string queue = "",
			CancellationToken cancellationToken = default)
			where T : IEvent;

		/// <summary>
		/// Subscribe a message
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="handleMessage"></param>
		/// <param name="queue"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task ConsumeAsync<T>(
			Func<T, Task> handleMessage,
			string queue = "",
			CancellationToken cancellationToken = default)
			where T : IEvent;

		/// <summary>
		/// Dispose channel and connection
		/// </summary>
		void Dispose();
	}
}
