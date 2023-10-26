using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;

namespace RideSharing.Common.MessageQueues.Receiver
{
    public abstract class RabbitMQReceiver<T> : RabbitMQBase where T : class
    {
        protected BlockingCollection<T> messages = new();
        //private readonly ManualResetEventSlim _eventSlim = new();

        public RabbitMQReceiver(string exchange) : this(exchange, null)
        {
        }

        public RabbitMQReceiver(string exchange, string? routingKey) : base(exchange, routingKey)
        {
        }

        public void Start(Func<T, Task> action)
        {
            var factory = new ConnectionFactory { HostName = hostName };

            Task.Run(async () =>
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

                        // declare a server-named queue
                        var queueName = channel.QueueDeclare().QueueName;
                        channel.QueueBind(queue: queueName,
                                          exchange: exchange,
                                          routingKey: routingKey);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            var obj = JsonConvert.DeserializeObject<T>(message);
                            messages.Add(obj);
                        };

                        channel.BasicConsume(queue: queueName,
                                             autoAck: true,
                                             consumer: consumer);

                        // start processor..
                        await this.ProcessMessageAsync(action);

                        //_eventSlim.Wait();
                    }
                }
            });
        }

        private async Task ProcessMessageAsync(Func<T, Task> action)
        {
            foreach (var message in messages.GetConsumingEnumerable())
            {
                await action(message);
            }
        }

        public void Stop()
        {
            //_eventSlim.Set();
            //_eventSlim.Dispose();
            messages.CompleteAdding();
        }
    }
}