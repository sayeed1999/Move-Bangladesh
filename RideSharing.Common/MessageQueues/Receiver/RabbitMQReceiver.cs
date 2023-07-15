using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace RideSharing.Common.MessageQueues.Receiver
{
    public abstract class RabbitMQReceiver<T> : RabbitMQBase where T : class
    {
        protected BlockingCollection<T> messages = new();
        //private readonly ManualResetEventSlim _eventSlim = new();

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
                            byte[] body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            T obj = JsonConvert.DeserializeObject<T>(message);
                            messages.Add(obj);
                        };

                        channel.BasicConsume(queue: queueName,
                                             autoAck: true,
                                             consumer: consumer);

                        // start processor..
                        this.ProcessMessage();

                        //_eventSlim.Wait();
                    }
                }
            });
        }

        /// <summary>
        /// Implement consuming thread for messages blocking collection receiver
        /// </summary>
        protected abstract void ProcessMessage();

        public void Stop()
        {
            //_eventSlim.Set();
            //_eventSlim.Dispose();
            messages.CompleteAdding();
        }
    }
}
