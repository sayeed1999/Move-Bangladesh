using BlogService.Entity;
using BlogService.Entity.Entities;
using BlogService.Infrastructure;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.UserRelationRepository
{
    public class UserRelationRepository : BaseRepository<UserRelation>, IUserRelationRepository
    {
        public UserRelationRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
