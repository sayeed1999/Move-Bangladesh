using AutoMapper;
using BlogService.Entity;
using BlogService.Infrastructure;
using BlogService.Service.UserService;
using RideSharing.Common.MessageQueues.Messages;

namespace BlogService.API.MessageQueues.Actions
{
    public class Actions
    {
        private readonly IMapper _mapper;
        private readonly IUserService userService;

        public Actions(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            this.userService = userService;
        }

        public async Task OnUserRegistered(UserRegistered message)
        {
            var user = _mapper.Map<User>(message);

            try
            {
                await userService.AddAsync(user);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task OnUserModified(UserModified message)
        {
            var user = _mapper.Map<User>(message);

            // TODO:- update user
        }
    }
}