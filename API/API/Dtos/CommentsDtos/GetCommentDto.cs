using System;

namespace API.Dtos.CommentsDtos
{
    public class GetCommentDto
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public string UserMainPhoto { get; set; }
        public string Body { get; set; }
        public int LikesCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
