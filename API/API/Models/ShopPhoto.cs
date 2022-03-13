using System;

namespace API.Models
{
    public class ShopPhoto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public Guid ShopId { get; set; }
        public bool IsMain { get; set; } = false;
    }
}
