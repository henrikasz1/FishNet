using System;

namespace API.Dtos.EventDtos
{
    public class AddEventDto
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Location { get; set; }
        
        public DateTime StartsAt { get; set; }
        
        public DateTime EndsAt { get; set; }
    }
}