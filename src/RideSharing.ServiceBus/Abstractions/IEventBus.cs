namespace RideSharing.ServiceBus.Abstractions
{
	public interface IEventBus
	{
		/// <summary>
		/// Use this method to initialize your service bus into your service.
		/// </summary>
		/// <param name="exchange"></param>
		/// <param name="routingKey"></param>
		void Initialize(string exchange, string? routingKey);

		/// <summary>
		/// Use this to implement event bus publisher.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="integrationEvent"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task PublishAsync<T>(
			T integrationEvent,
			CancellationToken cancellationToken = default)
			where T : class, IIntegrationEvent;

		/// <summary>
		/// Use this to implement event bus subscriber.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="integrationEvent"></param>
		/// <param name="action"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task ConsumeAsync<T>(
			T integrationEvent,
			Func<T, Task> handleMessage,
			CancellationToken cancellationToken = default)
			where T : class, IIntegrationEvent;
	}
}
