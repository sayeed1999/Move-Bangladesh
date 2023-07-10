using System.Collections.Concurrent;
using System.Text;
using RabbitMQ.Client;

namespace RideSharing.Common.MessageQueues.Emitter
{
    public class RabbitMQEmitter : RabbitMQBase
    {
        private BlockingCollection<string> messages = new();

        public RabbitMQEmitter(string exchange) : this(exchange, null) { }

        public RabbitMQEmitter(string exchange, string? routingKey) : base(exchange, routingKey) { }

        public void Start()
        {
            var factory = new ConnectionFactory { HostName = hostName };

            Task.Run(() =>
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: exchange, type: exchangeType);

                        foreach (var message in messages.GetConsumingEnumerable())
                        {
                            var body = Encoding.UTF8.GetBytes(message);
                            channel.BasicPublish(exchange: exchange,
                                                 routingKey: routingKey,
                                                 basicProperties: null,
                                                 body: body);
                        }
                    }
                }
            });
        }

        public void EnqueueMessage(string message)
        {
            messages.Add(message);
        }

        public void Stop()
        {
            messages.CompleteAdding();
        }
    }
}
