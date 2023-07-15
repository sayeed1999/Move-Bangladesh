using RideSharing.Common.MessageQueues.Messages;
using RideSharing.Common.MessageQueues.Receiver;

namespace BlogService.API.MessageQueues.Receiver
{
    public class UserRegisteredConsumer : RabbitMQReceiver<UserRegistered>
    {
        public UserRegisteredConsumer() : base("user-registered")
        {
        }

        protected override void ProcessMessage()
        {
            foreach (UserRegistered message in messages.GetConsumingEnumerable())
            {
                // TODO:- do something..
            }
        }
    }
}
