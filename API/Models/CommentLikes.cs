using System;

namespace API.Models
{
    public class CommentLikes
    {
        public Guid ObjectId { get; set; }
        public Guid LoverId { get; set; }
    }
}
