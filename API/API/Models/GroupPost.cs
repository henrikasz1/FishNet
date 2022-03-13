using System;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class GroupPost
    {
        public Guid PostId { get; set; }
        [JsonIgnore]
        public Guid GroupId { get; set; }
    }
}
