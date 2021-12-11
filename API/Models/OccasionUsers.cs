using System;
using Newtonsoft.Json;

namespace API.Models
{
    public class OccasionUser
    {
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        public Guid OccasionId { get; set; }
        [JsonIgnore]
        public Occasion Occasion { get; set; }
    }
}