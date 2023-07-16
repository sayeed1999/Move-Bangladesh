using AutoMapper;
using BlogService.API.Helpers.MapperProfiles;
using BlogService.Entity;
using BlogService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RideSharing.Common.MessageQueues.Messages;

namespace BlogService.API.MessageQueues.Actions
{
    public class Actions
    {
        private readonly IMapper _mapper;
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public Actions()
        {
            _mapper = new AutoMapperProfile().GetMapperInstance();
            _dbContextOptions = new DbContextOptions<AppDbContext>();
            
        }

        public async Task OnUserRegistered(UserRegistered message)
        {
            User user = _mapper.Map<User>(message);
            // create an instance of AppDbContext
            using (var dbContext = new AppDbContext(_dbContextOptions))
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task OnUserModified(UserModified message)
        {
            User user = _mapper.Map<User>(message);
            // create an instance of AppDbContext
            using (var dbContext = new AppDbContext(_dbContextOptions))
            {
                // TODO:- update user
            }
        }
    }
}
