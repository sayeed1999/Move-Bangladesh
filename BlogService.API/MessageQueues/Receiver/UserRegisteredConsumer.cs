using RideSharing.Common.MessageQueues.Messages;
using RideSharing.Common.MessageQueues.Receiver;

namespace BlogService.API.MessageQueues.Receiver
{
    public class UserRegisteredConsumer : RabbitMQReceiver<UserRegistered>
    {
        public UserRegisteredConsumer() : base("user-registered")
        {
        }
    }
}