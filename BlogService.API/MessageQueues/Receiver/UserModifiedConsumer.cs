using RideSharing.Common.MessageQueues.Messages;

namespace RideSharing.Common.MessageQueues.Receiver
{
    public class UserModifiedConsumer : RabbitMQReceiver<UserModified>
    {
        public UserModifiedConsumer() : base("user-modified")
        {
        }
    }
}