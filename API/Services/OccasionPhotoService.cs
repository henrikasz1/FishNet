using System;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using API.Infrastracture;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class OccasionPhotoService : IOccasionPhotoService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IPhotoAccessor _photoAccessor;

        public OccasionPhotoService(
            DataContext dataContext,
            IUserAccessorService userAccessorService,
            IPhotoAccessor photoAccessor)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
            _photoAccessor = photoAccessor;
        }
        
        public async Task SaveOccasionPhoto(IFormFile file, Guid occasionId)
        {
            var occasion = await _dataContext.Occasions.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.OccasionId == occasionId);

            if (occasion == null) throw new Exception("Invalid occasion");

            var photoUploadResult = await _photoAccessor.AddPhoto(file);
            var photo = new OccasionPhoto
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId,
                OccasionId = occasion.OccasionId,
            };

            if (!occasion.Photos.Any(x => x.IsMain))
            {
                photo.IsMain = true;
            }
            
            occasion.Photos.Add(photo);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Failed to add");
            }
            else
            {
                
            }
        }
        
        public async Task DeleteOccasionPhotoById(string photoId)
        {
            var photo = await _dataContext.OccasionsPhotos.FindAsync(photoId);

            var occasion = await _dataContext.Occasions.FirstOrDefaultAsync(x => x.OccasionId == photo.OccasionId);

            if (occasion.HostId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Unauthorized to delete this photo");
            }

            if (occasion.Photos.Count == 1)
            {
                throw new Exception("You cannot delete the picture");
            }

            await _photoAccessor.DeletePhoto(photo.Id);
            
            _dataContext.OccasionsPhotos.Remove(photo);

            await _dataContext.SaveChangesAsync();
        }

        public async Task MakeOccasionPhotoMainById(string photoId)
        {
            var photo = await _dataContext.OccasionsPhotos.FindAsync(photoId);
            var occasion = await _dataContext.Occasions
                .FirstOrDefaultAsync(x => x.OccasionId == photo.OccasionId);
            
            if (occasion.HostId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Unauthorized to select this photo as main");
            }
            
            var mainPhoto = await _dataContext.OccasionsPhotos
                .Where(x => x.IsMain == true)
                .FirstOrDefaultAsync();
            
            mainPhoto.IsMain = false;
            photo.IsMain = true;
            
            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Failed to make the photo as main");
            }
        }
    }
}