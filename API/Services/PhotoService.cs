using API.Dtos.Responses;
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

        public async Task<PhotoUploadResponse> SaveUserPhoto(IFormFile file)
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
                return photoUploadResult;
            }
            
            return null;
        }

        public async Task SavePostPhoto(IFormFile file, Guid postId)
        {
            var post = await _dataContext.Posts.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.PostId == postId);

            if (post == null) throw new Exception("Invalid post");

            var photoUploadResult = await _photoAccessor.AddPhoto(file);
            
            var photo = new Photo
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId,
            };

            post.Photos.Add(photo);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Failed to add");
            }
        }
    }
}
