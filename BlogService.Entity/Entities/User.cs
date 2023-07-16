using RideSharing.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Entity
{
    // TODO:- fix Audit issue in OnModelCreating()..
    public class User// : Audit
    {
        public long Id {  get; set; }
        public long AuthUserId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Phone { get; set; } = "";
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
    }
}
