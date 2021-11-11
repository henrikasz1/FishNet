using System;

namespace API.Models
{
    public class UserPhoto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string Body { get; set; }
        public int LikesCount { get; set; } = 0;
        public Guid UserId { get; set; }
    }
}
