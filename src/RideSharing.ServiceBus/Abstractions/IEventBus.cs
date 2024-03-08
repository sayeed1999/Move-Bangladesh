﻿namespace RideSharing.ServiceBus.Abstractions
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
			CancellationToken cancellationToken = default)
			where T : struct, IIntegrationEvent;

		/// <summary>
		/// Use this to implement event bus subscriber.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="integrationEvent"></param>
		/// <param name="action"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task ConsumeAsync<T>(
			T integrationEvent,
			Func<T, Task> handleMessage,
			CancellationToken cancellationToken = default)
			where T : struct, IIntegrationEvent;
	}
}
