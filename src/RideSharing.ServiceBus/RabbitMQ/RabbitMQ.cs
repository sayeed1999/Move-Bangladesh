using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RideSharing.ServiceBus.Abstractions;
using System.Text;
using System.Text.Json;

namespace RideSharing.ServiceBus.RabbitMQ
{
	public class RabbitMQ : IEventBus
	{
		private IConnectionFactory _factory;

		protected readonly string hostName;
		protected readonly string exchange;
		protected readonly string routingKey;
		protected readonly string exchangeType;

		//TODO:- add topic exchange support

		public RabbitMQ(string exchange, string? routingKey)
		{
			// TODO:- take the hostname from appsettings.json
			hostName = hostName ?? "localhost";
			exchange = exchange ?? string.Empty;
			routingKey = routingKey ?? string.Empty;

			exchangeType =
				routingKey == string.Empty
				? ExchangeType.Fanout
				: ExchangeType.Direct;
		}

		void IEventBus.Initialize()
		{
			_factory = new ConnectionFactory() { HostName = hostName };

			using (var connection = _factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.ExchangeDeclare(exchange, exchangeType);
			}
		}

		Task IEventBus.PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken)
		{
			using (var connection = _factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				var message = JsonSerializer.Serialize(integrationEvent);
				var body = Encoding.UTF8.GetBytes(message);
				channel.BasicPublish(exchange: exchange,
									routingKey: routingKey,
									basicProperties: null,
									body: body);
			}

			return Task.CompletedTask;
		}

		Task IEventBus.ConsumeAsync<T>(T integrationEvent, Func<T, Task> handleMessage, CancellationToken cancellationToken)
		{
			using (var connection = _factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.ExchangeDeclare(exchange: exchange,
										type: exchangeType,
										durable: true,
										autoDelete: false);

				// declare a server-named queue
				var queueName = channel.QueueDeclare().QueueName;
				channel.QueueBind(queue: queueName,
								  exchange: exchange,
								  routingKey: routingKey);

				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += async (model, ea) =>
				{
					var body = ea.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);
					Console.WriteLine(" [x] Received {0}", message);

					var integrationEvent = JsonSerializer.Deserialize<T>(message);

					// perform certain action on the message.
					if (integrationEvent is not null)
					{
						await handleMessage(integrationEvent);
					}
				};

				channel.BasicConsume(queue: queueName,
									autoAck: true,
									consumer: consumer);
			}

			return Task.CompletedTask;
		}
	}
}
