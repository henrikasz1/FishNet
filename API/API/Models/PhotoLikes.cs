using System;

namespace API.Models
{
    public class PhotoLikes
    {
        public string ObjectId { get; set; }
        public Guid LoverId { get; set; }
    }
}
