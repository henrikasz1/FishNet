using System;

namespace API.Dtos.EventDtos
{
    public class EditOccasionDto
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Location { get; set; }
        
        public DateTime StartsAt { get; set; }
        
        public DateTime EndsAt { get; set; }
    }
}