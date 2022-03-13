using API.Models;
using System;
using System.Collections.Generic;

namespace API.Dtos
{
    public class GetPostDto
    {
        public Guid PostId { get; set; }

        public Guid UserId { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public int LikesCount { get; set; }

        public List<PostPhoto> Photos { get; set; }
    }
}
