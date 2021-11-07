using API.Infrastracture;
using API.Models;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly DataContext _dataContext;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessorService _userAccessorService;

        public PhotoService(
            DataContext dataContext,
            IPhotoAccessor photoAccessor,
            IUserAccessorService userAccessorService)
        {
            _dataContext = dataContext;
            _photoAccessor = photoAccessor;
            _userAccessorService = userAccessorService;
        }

        public async Task<Photo> SavePhoto(IFormFile file)
        {
            var user = await _dataContext.Users.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.UserId == Guid.Parse(_userAccessorService.GetCurrentUserId()));

            if (user == null) return null;

            var photoUploadResult = await _photoAccessor.AddPhoto(file);

            var photo = new Photo
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId,
            };

            if (!user.Photos.Any(x => x.IsMain))
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if(result)
            {
                return photo;
            }
            
            return null;
        }
    }
}
