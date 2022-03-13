using System;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string Body { get; set; }
        public int LikesCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
    }
}