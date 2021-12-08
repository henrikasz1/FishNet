using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Services;

namespace API.Models
{
    public class User : IdentityUser
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public bool IsProfilePrivate { get; set; } = false;

        public List<Post> Posts { get; set; }
        
        public List<Occasion> Events { get; set; }

        public List<Comment> Comments { get; set; }

        public List<UserPhoto> Photos { get; set; }
    }
}
