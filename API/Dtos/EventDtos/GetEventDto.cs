﻿using System;

namespace API.Dtos.EventDtos
{
    public class GetEventDto
    {
        public Guid EventId { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Location { get; set; }

        public DateTime StartsAt { get; set; }
        
        public DateTime EndsAt { get; set; }
        
        // public List<Post> Posts {get; set;}
    }
}