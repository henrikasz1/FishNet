using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Occasion
    {
        [Required]
        public Guid OccasionId { get; set; }
        [Required]
        public Guid HostId { get; set; }
        //public User User { get; set; }//?
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public List<OccasionPhoto> Photos { get; set; }
        public List<OccasionUser> Participants { get; set; }
        public List<OccasionPost> Posts { get; set; }
        public int ParticipantsCount { get; set; } = 0;
    }
}