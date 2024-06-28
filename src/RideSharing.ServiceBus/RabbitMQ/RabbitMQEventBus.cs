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

		public void Initialize()
		{
			var factory = new ConnectionFactory() { HostName = hostName ?? "localhost" };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
		}

		public virtual Task PublishAsync<T>(
			T integrationEvent,
			string queue = "",
			CancellationToken cancellationToken = default)
			where T : class
		{
			try
			{
				var queueName = queue ?? integrationEvent.GetType().Name;

				_channel.QueueDeclare(queue: queueName,
									 durable: false,
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
			where T : class
		{
			var queueName = queue ?? typeof(T).Name;

			try
			{
				_channel.QueueDeclare(queue: queueName,
									 durable: false,
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
				};

				_channel.BasicConsume(queue: queueName,
									autoAck: true,
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
