using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RideSharing.ServiceBus.Abstractions;
using System.Text;
using System.Text.Json;

namespace RideSharing.ServiceBus.RabbitMQ
{
	public class RabbitMQEventBus : IEventBus
	{
		private readonly ILogger<RabbitMQEventBus> _logger;
		private IConnection _connection;
		private IModel _channel;

		private bool _queueDurablility = true; // should the queue survive node restart

		protected string hostName { get; private set; }
		protected string exchange { get; private set; }
		protected string routingKey { get; private set; }
		protected string exchangeType { get; private set; }

		//TODO:- add topic exchange support

		public RabbitMQEventBus(ILogger<RabbitMQEventBus> logger)
		{
			_logger = logger;
			Initialize();
		}

		private void Initialize()
		{
			var factory = new ConnectionFactory() { HostName = hostName ?? "localhost" };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();

			// Enable Fair Dispatch (not send more than one tasks to a queue, rather find an empty queue in round-robin fashion.
			_channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
		}

		public virtual Task PublishAsync<T>(
			T integrationEvent,
			string queue = "",
			CancellationToken cancellationToken = default)
			where T : IEvent
		{
			try
			{
				var queueName = queue ?? integrationEvent.GetType().Name;

				_channel.QueueDeclare(queue: queueName,
									 durable: _queueDurablility,
									 exclusive: false,
									 autoDelete: false,
									 arguments: null);

				var message = JsonSerializer.Serialize(integrationEvent);
				var body = Encoding.UTF8.GetBytes(message);

				_channel.BasicPublish(exchange: string.Empty,
									routingKey: queueName,
									basicProperties: null,
									body: body);

				_logger.LogInformation(" [x] Published {0}", message);
			}
			catch (Exception ex)
			{
				_logger.LogCritical("Exception occurred at RabbitMQ publisher: ", ex);
			}

			return Task.CompletedTask;
		}

		public virtual async Task ConsumeAsync<T>(
			Func<T, Task> handleMessage,
			string queue = "",
			CancellationToken cancellationToken = default)
			where T : IEvent
		{
			var queueName = queue ?? typeof(T).Name;

			try
			{
				_channel.QueueDeclare(queue: queueName,
									 durable: _queueDurablility,
									 exclusive: false,
									 autoDelete: false,
									 arguments: null);

				var consumer = new EventingBasicConsumer(_channel);
				consumer.Received += async (model, ea) =>
				{
					var body = ea.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);
					Console.WriteLine(" [x] Received {0}", message);

					var integrationEvent = JsonSerializer.Deserialize<T>(message);

					// perform certain action on the message.
					await handleMessage(integrationEvent);

					// Manual acknowledgement: tell rabbitmq to empty the item once consumer has finished processing without any failure.
					_channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
				};

				_channel.BasicConsume(queue: queueName,
									// Tell rabbitmq not to instantly empty the item from queue on received.
									autoAck: false,
									consumer: consumer);
			}
			catch (Exception ex)
			{
				_logger.LogCritical("Exception occurred at RabbitMQ Consumer: ", ex);
			}
		}

		public void Dispose()
		{
			_channel.Dispose();
			_connection.Dispose();
		}
	}
}
