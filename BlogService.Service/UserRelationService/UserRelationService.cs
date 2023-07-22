using BlogService.Entity;
using BlogService.Entity.Entities;
using BlogService.Service.UserRelationRepository;
using BlogService.Service.UserRelationService;
using BlogService.Service.UserRepository;
using RideSharing.Common.Entities;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.UserService
{
    public class UserRelationService : BaseService<UserRelation>, IUserRelationService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserRelationRepository userRelationRepository;

        public UserRelationService(
            IUserRepository userRepository,
            IUserRelationRepository userRelationRepository
        ) : base(userRelationRepository)
        {
            this.userRepository = userRepository;
            this.userRelationRepository = userRelationRepository;
        }

        public async Task<UserRelation> RequestFriendship(UserRelation userRelation)
        {
            userRelation.RelationType = RelationType.FriendRequestSent;

            // TODO:- 1. parse current user and set to from user id

            if (userRelation.FromUserId == userRelation.ToUserId)
                throw new IOException("Two users cannot be same!", 400);

            // check if to any user doesn't
            var userInDB = await this.userRepository.FindByIdAsync(userRelation.FromUserId);
            if (userInDB is null)
                throw new IOException("First user doesn't exist!", 400);

            userInDB = await this.userRepository.FindByIdAsync(userRelation.ToUserId);
            if (userInDB is null)
                throw new IOException("Second user doesn't exist!", 400);

            // check if one has already a pending request on another
            var userRelationInDB = await this.userRelationRepository.FirstOrDefaultAsync(
                filter: x => (x.FromUserId == userRelation.FromUserId 
                              && x.ToUserId == userRelation.ToUserId
                              && x.RelationType == RelationType.FriendRequestSent)
                              || (x.FromUserId == userRelation.ToUserId
                              && x.ToUserId == userRelation.FromUserId
                              && x.RelationType == RelationType.FriendRequestSent)
            );
            if (userRelationInDB is not null)
                throw new CustomException("Already has a pending friend request!", 400);

            await this.AddAsync(userRelation);
            return userRelation;
        }
    }
}
