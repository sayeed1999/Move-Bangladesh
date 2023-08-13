using RideSharing.Common.MessageQueues.Messages;
using RideSharing.Common.MessageQueues.Receiver;

namespace RideSharing.API.MessageQueues.Receiver
{
    public class UserModifiedConsumer : RabbitMQReceiver<UserModified>
    {
        public UserModifiedConsumer() : base("user-modified")
        {
        }
    }
}
