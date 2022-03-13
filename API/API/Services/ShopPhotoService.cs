using API.Dtos.PhotoDtos;
using API.Infrastracture;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ShopPhotoService : IShopPhotoService
    {
        private readonly DataContext _dataContext;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessorService _userAccessorService;

        public ShopPhotoService(
            DataContext dataContext,
            IPhotoAccessor photoAccessor,
            IUserAccessorService userAccessorService)
        {
            _dataContext = dataContext;
            _photoAccessor = photoAccessor;
            _userAccessorService = userAccessorService;
        }

        public async Task SaveShopPhoto(IFormFile file, Guid shopId)
        {
            var shopAdvert = await _dataContext.ShopAdverts
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.ShopId == shopId);

            if (shopAdvert == null) throw new Exception("Invalid advertisement");

            var photoUploadResult = await _photoAccessor.AddPhoto(file);

            var photo = new ShopPhoto
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId,
                ShopId = shopAdvert.ShopId,
            };

            shopAdvert.Photos.Add(photo);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Failed to add");
            }
        }

        public async Task DeleteShopPhotoById(string photoId)
        {
            var photo = await _dataContext.ShopPhotos.FindAsync(photoId);
            var shopPhoto = await _dataContext.ShopAdverts.FirstOrDefaultAsync(x => x.ShopId == photo.ShopId);

            if (shopPhoto.UserId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Unauthorized to delete this photo");
            }

            await _photoAccessor.DeletePhoto(photo.Id);
            _dataContext.ShopPhotos.Remove(photo);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<string> ChangeMainShopPhoto(string newMainPhotoId)
        {
            var photo = await _dataContext.ShopPhotos.FindAsync(newMainPhotoId);
            var shopAd = await _dataContext.ShopAdverts.FirstOrDefaultAsync(x => x.ShopId == photo.ShopId);

            if (shopAd.UserId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Unauthorized to select this photo as main");
            }

            var mainPhoto = await _dataContext.ShopPhotos
                .Where(x => x.IsMain == true)
                .FirstOrDefaultAsync();

            if (mainPhoto != null)
            {
                mainPhoto.IsMain = false;
            }

            photo.IsMain = true;

            var result = await _dataContext.SaveChangesAsync() > 0;

            return !result
                ? throw new DbUpdateException("Failed to make the photo as main")
                : "Photo has been changed to main successfully";
        }

        public async Task<GetShopPhotoDto> GetMainShopPhoto(Guid shopId)
        {
            var photo = await _dataContext.ShopPhotos
                .Where(x => x.ShopId == shopId && x.IsMain == true)
                .FirstOrDefaultAsync();

            return photo == null
                ? throw new Exception("Could not find requested photo")
                : new GetShopPhotoDto { Url = photo.Url };
        }

        public async Task DeleteShopPhoto(ShopPhoto photo)
        {
            await _photoAccessor.DeletePhoto(photo.Id);
            _dataContext.ShopPhotos.Remove(photo);
        }

    }
}
