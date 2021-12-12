﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Post
    {
        [Required]
        public Guid PostId { get; set; }

        [Required]
        public string Body { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public List<Comment> Comments { get; set; }

        public List<PostPhoto> Photos { get; set; }

        public int LikesCount { get; set; } = 0;
    }
}