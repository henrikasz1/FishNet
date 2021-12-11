using API.Dtos.ShopDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static API.Models.Enums.ShopEnum;

namespace API.Services.Interfaces
{
    public interface IShopService
    {
        Task AddShopAd(List<IFormFile> files, AddShopAdDto shopAd);
        Task<GetShopAdDto> GetShopAdById(Guid shopAdId);
        Task<IList<GetShopAdDto>> GetShopAdsByUserId(Guid userId);
        Task<IList<GetShopAdDto>> GetAllShopAds();
        Task<IList<GetShopAdDto>> GetShopAdsByName(string filter);
        Task<string> DeleteShopAdById(Guid Id);
        Task UpdateShopAdById(Guid Id, EditShopAdDto newShopAd);
        Task<IList<GetShopAdDto>> GetShopAdByCategory(ProductType category);
    }
}
