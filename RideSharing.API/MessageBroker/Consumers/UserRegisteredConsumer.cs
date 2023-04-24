using MassTransit;
using Newtonsoft.Json;
using RideSharing.Common.MessageBroker.Messages;

namespace RideSharing.API.MessageBroker.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"User registered: {jsonMessage}");
        }
    }
}
