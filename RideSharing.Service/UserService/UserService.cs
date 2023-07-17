using RideSharing.Entity;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;

namespace RideSharing.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseRepository<User> repository) : base(repository)
        {
        }
    }
}