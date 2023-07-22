using Microsoft.AspNetCore.Identity;
using RideSharing.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace RideSharing.Entity
{
    public class User : Base
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
    }
}