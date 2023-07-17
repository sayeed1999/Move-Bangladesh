using RabbitMQ.Client;

namespace RideSharing.Common.MessageQueues
{
    public class RabbitMQBase
    {
        protected readonly string hostName;
        protected readonly string exchange;
        protected readonly string routingKey;
        protected readonly string exchangeType;

        //TODO:- add topic exchange support

        public RabbitMQBase(string exchange, string? routingKey)
        {
            // TODO:- take the hostname from appsettings.json
            this.hostName = hostName ?? "localhost";
            this.exchange = exchange ?? string.Empty;
            this.routingKey = routingKey ?? string.Empty;

            this.exchangeType =
                this.routingKey == string.Empty
                ? ExchangeType.Fanout
                : ExchangeType.Direct;
        }
    }
}