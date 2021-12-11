using System;
using Newtonsoft.Json;

namespace API.Models
{
    public class OccasionUser
    {
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Guid OccasionId { get; set; }
    }
}