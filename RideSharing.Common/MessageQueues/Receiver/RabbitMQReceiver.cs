using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace RideSharing.Common.MessageQueues.Receiver
{
    public class RabbitMQReceiver : RabbitMQBase
    {
        private readonly ManualResetEventSlim _eventSlim = new();

        public RabbitMQReceiver(string exchange) : this(exchange, null) { }

        public RabbitMQReceiver(string exchange, string? routingKey) : base(exchange, routingKey) { }

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

                        // declare a server-named queue
                        var queueName = channel.QueueDeclare().QueueName;
                        channel.QueueBind(queue: queueName,
                                          exchange: exchange,
                                          routingKey: routingKey);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            byte[] body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                        };

                        channel.BasicConsume(queue: queueName,
                                             autoAck: true,
                                             consumer: consumer);

                        _eventSlim.Wait();
                    }
                }
            });
        }

        public void Stop()
        {
            _eventSlim.Set();
            _eventSlim.Dispose();
        }
    }
}
