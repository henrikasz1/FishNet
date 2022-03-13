using System;
using System.Collections.Generic;
using API.Models;
using Newtonsoft.Json;

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
        
        public List<Guid> ParticipantsIds { get; set; }
        
        public List<OccasionPhoto> Photos { get; set; }
        
        public int ParticipantsCount { get; set; } = 0;
    }
}