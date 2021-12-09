using System;

namespace API.Models
{
    public class OccasionPhoto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string Body { get; set; }
        public int LikesCount { get; set; } = 0;
        public Guid OccasionId { get; set; }
    }
}