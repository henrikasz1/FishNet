using static API.Models.Enums.ShopEnum;

namespace API.Dtos.ShopDtos
{
    public class AddShopAdDto
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
    }
}
