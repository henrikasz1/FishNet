using System;

namespace API.Models
{
    public class PostPhoto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public int LikesCount { get; set; } = 0;
        public Guid PostId { get; set; }
    }
}
