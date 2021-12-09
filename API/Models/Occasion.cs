using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Occasion
    {
        [Required]
        public Guid OccasionId { get; set; }
        
        [Required]
        public Guid HostId { get; set; }
        
        public User User { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Location { get; set; }

        public DateTime StartsAt { get; set; }
        
        public DateTime EndsAt { get; set; }
        
        public List<OccasionPhoto> Photo { get; set; }

       // public List<Post> Posts { get; set; }
        
        //public List<User> UsersAttending { get; set; }
        
       // public 
    }
}