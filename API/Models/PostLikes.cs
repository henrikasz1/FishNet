using System;

namespace API.Models
{
    public class PostLikes
    {
        public Guid ObjectId { get; set; }
        public Guid LoverId { get; set; }
    }
}
