using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Entity.Entities
{
    public class UserRelation
    {
        public long Id { get; set; }
        public RelationType RelationType { get; set; } = RelationType.FriendRequestSent;
        public User FromUser { get; set; }
        public long FromUserId { get; set; }
        public User ToUser { get; set; }
        public long ToUserId { get; set; }
    }
}
