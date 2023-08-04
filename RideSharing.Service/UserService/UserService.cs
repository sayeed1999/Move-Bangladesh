using RideSharing.Entity;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;

namespace RideSharing.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IBaseRepository<User> userRepository;
        
        public UserService( 
            IBaseRepository<User> userRepository
        ) : base(userRepository)
        {
            this.userRepository = userRepository;
        }
    }
}