using AutoMapper;
using RideSharing.Common.MessageQueues.Messages;
using RideSharing.Entity;
using RideSharing.Infrastructure;

namespace RideSharing.API.MessageQueues.Actions
{
    public class Actions
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public Actions(IMapper mapper, ApplicationDbContext dbContext)
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
