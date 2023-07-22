using BlogService.Entity;
using BlogService.Entity.Entities;
using Sayeed.NTier.Generic.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.UserRelationService
{
    public interface IUserRelationService : IBaseService<UserRelation>
    {
        Task<UserRelation> RequestFriendship(UserRelation userRelation);
    }
}
