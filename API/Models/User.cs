using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

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

       // public DateTime DateOfBirth { get; set; }

        //public List<Post> Posts { get; set; }

        //public List<Comment> Comments { get; set; }

        //public List<Photo> Photos { get; set; }
    }
}
