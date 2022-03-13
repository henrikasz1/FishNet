using System;

namespace API.Models
{
    public class OccasionPhoto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public Guid OccasionId { get; set; }
        public bool IsMain { get; set; } = false;
    }
}