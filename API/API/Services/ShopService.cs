using API.Dtos.ShopDtos;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static API.Models.Enums.ShopEnum;

namespace API.Services
{
    public class ShopService : IShopService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IShopPhotoService _photoService;

        public ShopService(DataContext dataContext, IUserAccessorService userAccessorService, IShopPhotoService photoService)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
            _photoService = photoService;
        }

        public async Task AddShopAd(List<IFormFile> files, AddShopAdDto shopAd)
        {
            var userId = _userAccessorService.GetCurrentUserId();

            var newShopAd = new Shop()
            {
                ShopId = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                ProductName = shopAd.ProductName,
                Price = shopAd.Price,
                Location = shopAd.Location,
                Description = shopAd.Description,
                ProductType = shopAd.ProductType,
                CreatedAt = DateTime.Now,
            };

            _dataContext.ShopAdverts.Add(newShopAd);

            var result = await _dataContext.SaveChangesAsync() > 0;

            foreach (var photo in files)
            {
                await _photoService.SaveShopPhoto(photo, newShopAd.ShopId);
            }

            if (!result)
            {
                throw new DbUpdateException("Failed to add");
            }
        }

        public async Task<GetShopAdDto> GetShopAdById(Guid shopAdId)
        {
            var shopAd = await _dataContext.ShopAdverts
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.ShopId == shopAdId);

            var shopAdDto = new GetShopAdDto
            {
                ShopId = shopAdId,
                UserId = shopAd.UserId,
                ProductName = shopAd.ProductName,
                Price = shopAd.Price,
                Location = shopAd.Location,
                Description = shopAd.Description,
                ProductType = shopAd.ProductType,
                CreatedAt = DateTime.Now,
                LikesCount = shopAd.LikesCount,
                Photos = shopAd.Photos
            };

            return shopAdDto;
        }

        public async Task<IList<GetShopAdDto>> GetShopAdsByUserId(Guid userId)
        {
            var shopAdsDtoList = new List<GetShopAdDto>();

            var shopAds = await _dataContext.ShopAdverts.Where(x => x.UserId == userId)
                .Include(y => y.Photos)
                .ToListAsync();

            foreach (var shopAd in shopAds)
            {
                shopAdsDtoList.Add(
                    new GetShopAdDto
                    {
                        ShopId = shopAd.ShopId,
                        UserId = shopAd.UserId,
                        ProductName = shopAd.ProductName,
                        Price = shopAd.Price,
                        Location = shopAd.Location,
                        Description = shopAd.Description,
                        ProductType = shopAd.ProductType,
                        CreatedAt = DateTime.Now,
                        LikesCount = shopAd.LikesCount,
                        Photos = shopAd.Photos
                    });
            }

            return shopAdsDtoList == null
                ? throw new Exception("Could not find any advertisements")
                : shopAdsDtoList;
        }

        public async Task<IList<GetShopAdDto>> GetAllShopAds()
        {
            var shopAdsDtoList = new List<GetShopAdDto>();

            var shopAds = await _dataContext.ShopAdverts
               .Include(y => y.Photos)
               .ToListAsync();

            foreach (var shopAd in shopAds)
            {
                shopAdsDtoList.Add(
                    new GetShopAdDto
                    {
                        ShopId = shopAd.ShopId,
                        UserId = shopAd.UserId,
                        ProductName = shopAd.ProductName,
                        Price = shopAd.Price,
                        Location = shopAd.Location,
                        Description = shopAd.Description,
                        ProductType = shopAd.ProductType,
                        CreatedAt = DateTime.Now,
                        LikesCount = shopAd.LikesCount,
                        Photos = shopAd.Photos
                    });
            }

            return shopAdsDtoList == null
                ? throw new Exception("Could not find any advertisements")
                : shopAdsDtoList;
        }

        public async Task<IList<GetShopAdDto>> GetShopAdsByName(string filter)
        {
            var shopAdsDtoList = new List<GetShopAdDto>();

            var shopAds = await _dataContext.ShopAdverts
               .Include(y => y.Photos)
               .Where(x => x.ProductName.ToLower().Contains(filter.ToLower()))
               .ToListAsync();

            foreach (var shopAd in shopAds)
            {
                shopAdsDtoList.Add(
                    new GetShopAdDto
                    {
                        ShopId = shopAd.ShopId,
                        UserId = shopAd.UserId,
                        ProductName = shopAd.ProductName,
                        Price = shopAd.Price,
                        Location = shopAd.Location,
                        Description = shopAd.Description,
                        ProductType = shopAd.ProductType,
                        CreatedAt = DateTime.Now,
                        LikesCount = shopAd.LikesCount,
                        Photos = shopAd.Photos
                    });
            }

            return shopAdsDtoList == null
                ? throw new Exception("Could not find any advertisements")
                : shopAdsDtoList;
        }

        public async Task<IList<GetShopAdDto>> GetShopAdByCategory(ProductType category)
        {
            var shopAdsDtoList = new List<GetShopAdDto>();

            var shopAds = await _dataContext.ShopAdverts
                .Include(x => x.Photos)
                .Where(x => x.ProductType == category)
                .ToListAsync();

            foreach (var shopAd in shopAds)
            {
                shopAdsDtoList.Add(
                    new GetShopAdDto
                    {
                        ShopId = shopAd.ShopId,
                        UserId = shopAd.UserId,
                        ProductName = shopAd.ProductName,
                        Price = shopAd.Price,
                        Location = shopAd.Location,
                        Description = shopAd.Description,
                        ProductType = shopAd.ProductType,
                        CreatedAt = DateTime.Now,
                        LikesCount = shopAd.LikesCount,
                        Photos = shopAd.Photos
                    });
            }

            return shopAdsDtoList == null
                ? throw new Exception("Could not find any advertisements")
                : shopAdsDtoList;
        }

        public async Task<string> DeleteShopAdById(Guid Id)
        {
            var userId = _userAccessorService.GetCurrentUserId();

            var shopAdToDelete = await _dataContext.ShopAdverts
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.ShopId == Id);

            if (shopAdToDelete.UserId != Guid.Parse(userId))
            {
                throw new UnauthorizedAccessException("User does not own this advertisement");
            }

            foreach (var photo in shopAdToDelete.Photos)
            {
                await _photoService.DeleteShopPhoto(photo);
            }

            _dataContext.ShopAdverts.Remove(shopAdToDelete);

            var result = await _dataContext.SaveChangesAsync() > 0;

            return !result
                ? throw new DbUpdateException("Unable to remove advertisement")
                : "Advertisement has been removed succesfully";
        }

        public async Task UpdateShopAdById(Guid Id, EditShopAdDto newShopAd)
        {
            var userId = _userAccessorService.GetCurrentUserId();
            var shopAd = await _dataContext.ShopAdverts.FirstOrDefaultAsync(x => x.ShopId == Id);

            if (shopAd.UserId != Guid.Parse(userId))
            {
                throw new UnauthorizedAccessException("User does not own this advertisement");
            }

            shopAd.ProductName = newShopAd.ProductName ?? shopAd.ProductName;
            shopAd.Location = newShopAd.Location ?? shopAd.Location;
            shopAd.Price = newShopAd.Price;
            shopAd.Description = newShopAd.Description ?? shopAd.Description;
            shopAd.ProductType = newShopAd.ProductType;

            var success = await _dataContext.SaveChangesAsync() > 0;

            if (!success)
            {
                throw new DbUpdateException("Could not update advertisement");
            }
        }
    }
}
