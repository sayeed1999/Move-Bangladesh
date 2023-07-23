using BlogService.Entity.Entities;
using RideSharing.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Entity
{
    public class User : Audit
    {
        public long AuthUserId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Phone { get; set; } = "";
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public HashSet<UserRelation> UserRelations { get; set; } = new();
    }
}
