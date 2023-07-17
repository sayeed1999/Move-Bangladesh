using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using System.Text;

namespace RideSharing.Common.MessageQueues.Emitter
{
    public abstract class RabbitMQEmitter<T> : RabbitMQBase
    {
        private BlockingCollection<T> messages = new();

        public RabbitMQEmitter(string exchange) : this(exchange, null)
        {
        }

        public RabbitMQEmitter(string exchange, string? routingKey) : base(exchange, routingKey)
        {
        }

        public void Start()
        {
            var factory = new ConnectionFactory { HostName = hostName };

            Task.Run(() =>
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(
                            exchange: exchange,
                            type: exchangeType,
                            durable: true,
                            autoDelete: false);

                        foreach (var message in messages.GetConsumingEnumerable())
                        {
                            var serialized = JsonConvert.SerializeObject(message);
                            var body = Encoding.UTF8.GetBytes(serialized);
                            channel.BasicPublish(exchange: exchange,
                                                 routingKey: routingKey,
                                                 basicProperties: null,
                                                 body: body);
                        }
                    }
                }
            });
        }

        public void EnqueueMessage(T message)
        {
            messages.Add(message);
        }

        public void Stop()
        {
            messages.CompleteAdding();
        }
    }
}