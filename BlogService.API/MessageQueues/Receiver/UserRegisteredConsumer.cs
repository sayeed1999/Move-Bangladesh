using AutoMapper;
using BlogService.API.Helpers.MapperProfiles;
using BlogService.Entity;
using RideSharing.Common.MessageQueues.Messages;
using RideSharing.Common.MessageQueues.Receiver;

namespace BlogService.API.MessageQueues.Receiver
{
    public class UserRegisteredConsumer : RabbitMQReceiver<UserRegistered>
    {
        private readonly IMapper _mapper;

        public UserRegisteredConsumer() : base("user-registered")
        {
            _mapper = new AutoMapperProfile().GetMapperInstance();
        }

        protected override void ProcessMessage()
        {
            foreach (UserRegistered message in messages.GetConsumingEnumerable())
            {
                var user = _mapper.Map<User>(message);
            }
        }
    }
}
