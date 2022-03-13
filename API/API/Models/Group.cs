using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Group
    {
        [Required]
        public Guid GroupId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        public User User { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public GroupPhoto Photo { get; set; }
        public List<GroupUser> Members { get; set; }
        public List<GroupPost> Posts { get; set; }
        public int MembersCount { get; set; } = 0;
    }
}