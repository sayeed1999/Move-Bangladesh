using AutoMapper;
using BlogService.Entity;
using BlogService.Infrastructure;
using RideSharing.Common.MessageQueues.Messages;

namespace BlogService.API.MessageQueues.Actions
{
    public class Actions
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public Actions(IMapper mapper, AppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task OnUserRegistered(UserRegistered message)
        {
            var user = _mapper.Map<User>(message);

            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
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