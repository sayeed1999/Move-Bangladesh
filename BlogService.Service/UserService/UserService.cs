using BlogService.Entity;
using BlogService.Entity.Entities;
using BlogService.Service.UserRelationRepository;
using BlogService.Service.UserRepository;
using Sayeed.Generic.OnionArchitecture.Logic;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.UserService
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserRelationRepository userRelationRepository;

        public UserService(
            IUserRepository userRepository,
            IUserRelationRepository userRelationRepository
        ) : base(userRepository)
        {
            this.userRepository = userRepository;
            this.userRelationRepository = userRelationRepository;
        }

    }
}
