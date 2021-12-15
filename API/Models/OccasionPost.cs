using System;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class OccasionPost
    {
        public Guid PostId { get; set; }
        [JsonIgnore]
        public Guid OccasionId { get; set; }
    }
}
