using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RideSharing.ServiceBus.Abstractions;
using System.Text;
using System.Text.Json;

namespace RideSharing.ServiceBus.RabbitMQ
{
	public class RabbitMQEventBus : IEventBus
	{
		private IConnectionFactory _factory;

		protected string hostName { get; private set; }
		protected string exchange { get; private set; }
		protected string routingKey { get; private set; }
		protected string exchangeType { get; private set; }

		//TODO:- add topic exchange support

		public RabbitMQEventBus()
		{
			_factory = new ConnectionFactory() { HostName = hostName ?? "localhost" };
		}

		public Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
			where T : struct, IIntegrationEvent
		{
			using (var connection = _factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				var queueName = integrationEvent.GetType().Name;

				channel.QueueDeclare(queue: queueName,
									 durable: false,
									 exclusive: false,
									 autoDelete: false,
									 arguments: null);

				var message = JsonSerializer.Serialize(integrationEvent);
				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(exchange: string.Empty,
									routingKey: queueName,
									basicProperties: null,
									body: body);
				Console.WriteLine(" [x] Published {0}", message);
			}

			return Task.CompletedTask;
		}

		public Task ConsumeAsync<T>(T integrationEvent, Func<T, Task> handleMessage, CancellationToken cancellationToken = default)
			where T : struct, IIntegrationEvent
		{
			var queueName = integrationEvent.GetType().Name;

			using (var connection = _factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: queueName,
									 durable: false,
									 exclusive: false,
									 autoDelete: false,
									 arguments: null);

				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += async (model, ea) =>
				{
					var body = ea.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);
					Console.WriteLine(" [x] Received {0}", message);

					var integrationEvent = JsonSerializer.Deserialize<T>(message);

					// perform certain action on the message.
					await handleMessage(integrationEvent);
				};

				channel.BasicConsume(queue: queueName,
									autoAck: true,
									consumer: consumer);
			}

			return Task.CompletedTask;
		}
	}
}
