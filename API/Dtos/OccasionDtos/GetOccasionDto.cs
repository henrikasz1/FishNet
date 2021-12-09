using System;

namespace API.Dtos.EventDtos
{
    public class GetOccasionDto
    {
        public Guid OccasionId { get; set; }

        public Guid HostId { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Location { get; set; }

        public DateTime StartsAt { get; set; }
        
        public DateTime EndsAt { get; set; }
        
        // public List<Post> Posts {get; set;}
    }
}