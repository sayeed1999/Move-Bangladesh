using RideSharing.Common.MessageQueues.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Common.MessageQueues.Emitter
{
    public class UserModifiedEmitter : RabbitMQEmitter<UserModified>
    {
        public UserModifiedEmitter() : base("user-modified")
        {
            
        }
    }
}
