using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static API.Models.Enums.ShopEnum;

namespace API.Models
{
    public class Shop
    {
        [Required]
        public Guid ShopId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }
        public string Location { get; set; }
        [Required]
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public List<ShopPhoto> Photos { get; set; }
        public int LikesCount { get; set; } = 0;
    }
}
