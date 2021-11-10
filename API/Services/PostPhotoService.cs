using API.Infrastracture;
using API.Models;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public class PostPhotoService : IPostPhotoService
    {
        private readonly DataContext _dataContext;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessorService _userAccessorService;

        public PostPhotoService(
            DataContext dataContext,
            IPhotoAccessor photoAccessor,
            IUserAccessorService userAccessorService)
        {
            _dataContext = dataContext;
            _photoAccessor = photoAccessor;
            _userAccessorService = userAccessorService;
        }

        public async Task SavePostPhoto(IFormFile file, Guid postId)
        {
            var post = await _dataContext.Posts.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.PostId == postId);

            if (post == null) throw new Exception("Invalid post");

            var photoUploadResult = await _photoAccessor.AddPhoto(file);

            var photo = new PostPhoto
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId,
                PostId = post.PostId
            };

            post.Photos.Add(photo);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Failed to add");
            }
        }

        public async Task DeletePostPhotoById(string photoId)
        {
            var photo = await _dataContext.PostPhotos.FindAsync(photoId);

            var photoPost = await _dataContext.Posts.FirstOrDefaultAsync(x => x.PostId == photo.PostId);

            if (photoPost.UserId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Unauthorized to delete this photo");
            }

            await _photoAccessor.DeletePhoto(photo.Id);
            _dataContext.PostPhotos.Remove(photo);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeletePostPhoto(PostPhoto photo)
        {
            await _photoAccessor.DeletePhoto(photo.Id);
            _dataContext.PostPhotos.Remove(photo);
        }
    }
}
