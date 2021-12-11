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
            var postPhotos = await _dataContext.PostPhotos.Where(x => x.PostId == postId).ToListAsync();
            var post = await _dataContext.Posts.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.PostId == postId);

            var isMain = postPhotos.Any() ? false : true;

            if (post == null) throw new Exception("Invalid post");

            var photoUploadResult = await _photoAccessor.AddPhoto(file);

            var photo = new PostPhoto
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId,
                PostId = post.PostId,
                IsMain = isMain
            };

            post.Photos.Add(photo);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Failed to add");
            }
        }

        public async Task<string> ChangeMainPostPhoto(string newMainPhotoId)
        {
            var photo = await _dataContext.PostPhotos.FindAsync(newMainPhotoId);

            var post = await _dataContext.Posts
                .FirstOrDefaultAsync(x => x.PostId == photo.PostId);

            if (post.UserId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Unauthorized to select this photo as main");
            }

            var mainPhoto = await _dataContext.PostPhotos
                .Where(x => x.IsMain == true)
                .FirstOrDefaultAsync();

            mainPhoto.IsMain = false;
            photo.IsMain = true;

            var result = await _dataContext.SaveChangesAsync() > 0;

            return !result
                ? throw new DbUpdateException("Failed to make the photo as main")
                : "Photo has been changed to main successfully";
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

       // public async Task

        public async Task DeletePostPhoto(PostPhoto photo)
        {
            await _photoAccessor.DeletePhoto(photo.Id);
            _dataContext.PostPhotos.Remove(photo);
        }
    }
}
