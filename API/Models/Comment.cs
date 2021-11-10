using System;

namespace API.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; }

        public Guid PostId { get; set; }

        public Guid UserId { get; set; }

        public Guid ParentId { get; set; }

        public string Text { get; set; }

        public int LikesCount { get; set; } = 0;
    }
}