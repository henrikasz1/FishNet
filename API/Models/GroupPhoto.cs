using System;

namespace API.Models
{
    public class GroupPhoto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public Guid GroupId { get; set; }
    }
}