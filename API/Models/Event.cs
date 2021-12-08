using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Event
    {
        [Required]
        public Guid EventId { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        
        public User User { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Location { get; set; }

        public DateTime StartsAt { get; set; }
        
        public DateTime EndsAt { get; set; }
        
       // public List<Post> Posts { get; set; }
        
        //public List<User> UsersAttending { get; set; }
    }
}