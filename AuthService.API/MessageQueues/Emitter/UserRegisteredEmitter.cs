using RideSharing.Common.MessageQueues.Emitter;
using RideSharing.Common.MessageQueues.Messages;

namespace AuthService.API.MessageQueues.Emitter
{
    public class UserRegisteredEmitter : RabbitMQEmitter<UserRegistered>
    {
        public UserRegisteredEmitter() : base("user-registered")
        {

        }
    }
}
