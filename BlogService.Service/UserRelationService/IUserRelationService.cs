using BlogService.Entity;
using BlogService.Entity.Entities;
using Sayeed.Generic.OnionArchitecture.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.UserRelationService
{
    public interface IUserRelationService : IBaseService<UserRelation>
    {
        Task<UserRelation> SendFriendRequest(UserRelation userRelation);
        Task<UserRelation> ResponseFriendRequest(UserRelation userRelation);
        Task<UserRelation> UnfriendUserAsync(UserRelation userRelation);
    }
}
