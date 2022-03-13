using System;
using Newtonsoft.Json;

namespace API.Models
{
    public class GroupUser
    {
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Guid GroupId { get; set; }
    }
}