using System;

namespace API.Models
{
    public class PostPhoto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public Guid PostId { get; set; }
        public bool IsMain { get; set; } = false;
    }
}
