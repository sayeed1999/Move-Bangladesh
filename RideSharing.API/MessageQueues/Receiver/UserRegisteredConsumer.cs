using RideSharing.Common.MessageQueues.Messages;
using RideSharing.Common.MessageQueues.Receiver;

namespace RideSharing.API.MessageQueues.Receiver
{
    public class UserRegisteredConsumer : RabbitMQReceiver<UserRegistered>
    {
        public UserRegisteredConsumer() : base("user-registered")
        {
        }
    }
}
