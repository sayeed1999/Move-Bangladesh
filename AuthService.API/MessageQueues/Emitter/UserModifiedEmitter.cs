using RideSharing.Common.MessageQueues.Emitter;
using RideSharing.Common.MessageQueues.Messages;

namespace AuthService.API.MessageQueues.Emitter
{
    public class UserModifiedEmitter : RabbitMQEmitter<UserModified>
    {
        public UserModifiedEmitter() : base("user-modified")
        {
        }
    }
}