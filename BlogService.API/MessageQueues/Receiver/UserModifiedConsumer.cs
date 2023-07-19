using RideSharing.Common.MessageQueues.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Common.MessageQueues.Receiver
{
    public class UserModifiedConsumer : RabbitMQReceiver<UserModified>
    {
        public UserModifiedConsumer() : base("user-modified")
        {
            
        }
    }
}
