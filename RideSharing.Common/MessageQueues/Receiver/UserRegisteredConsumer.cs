using RideSharing.Common.MessageQueues.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Common.MessageQueues.Receiver
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
