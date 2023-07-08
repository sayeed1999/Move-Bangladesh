using MassTransit;
using Newtonsoft.Json;
using RideSharing.Common.MessageBroker.Messages;

namespace RideSharing.API.MessageBroker.Consumers
{
    public class UserRegisteredConsumer : IConsumer<IUserRegistered>
    {
        public async Task Consume(ConsumeContext<IUserRegistered> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"User registered: {jsonMessage}");
        }
    }
}
