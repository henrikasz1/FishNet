using API.Models;
using System;
using System.Collections.Generic;
using static API.Models.Enums.ShopEnum;

namespace API.Dtos.ShopDtos
{
    public class GetShopAdDto
    {
        public Guid ShopId { get; set; }

        public Guid UserId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public ProductType ProductType { get; set; }

        public DateTime CreatedAt { get; set; }

        public int LikesCount { get; set; }

        public List<ShopPhoto> Photos { get; set; }
    }
}
