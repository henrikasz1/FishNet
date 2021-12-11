using API.Dtos.PhotoDtos;
using API.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IShopPhotoService
    {
        Task SaveShopPhoto(IFormFile file, Guid postId);
        Task DeleteShopPhoto(ShopPhoto photo);
        Task DeleteShopPhotoById(string photoId);
        Task<GetShopPhotoDto> GetMainShopPhoto(Guid shopId);
        Task<string> ChangeMainShopPhoto(string newMainPhotoId);
    }
}
