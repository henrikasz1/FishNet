using API.Dtos.Responses;
using API.Dtos.UserPhotoDtos;
using API.Infrastracture;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserPhotoService : IUserPhotoService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IPhotoAccessor _photoAccessor;

        public UserPhotoService(
            DataContext dataContext,
            IUserAccessorService userAccessorService,
            IPhotoAccessor photoAccessor)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
            _photoAccessor = photoAccessor;
        }

        public async Task<PhotoUploadResponse> SaveUserPhoto(IFormFile file, string body)
        {
            var user = await _dataContext.Users.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.UserId == Guid.Parse(_userAccessorService.GetCurrentUserId()));

            if (user == null) return null;

            var photoUploadResult = await _photoAccessor.AddPhoto(file);

            var photo = new UserPhoto
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId,
                UserId = user.UserId,
                Body = body
            };

            if (!user.Photos.Any(x => x.IsMain))
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (result)
            {
                return photoUploadResult;
            }

            return null;
        }

        public async Task DeleteUserPhoto(string photoId)
        {
            var photo = await _dataContext.UserPhotos.FindAsync(photoId);

            if (photo == null)
            {
                throw new NullReferenceException("Photo not found");
            }

            if (photo.UserId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Only owner can delete this photo");
            }

            _dataContext.UserPhotos.Remove(photo);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Could not delete photo");
            }
        }

        public async Task<IList<GetAllUserPhotosDto>> GetAllUserPhotos(Guid userId)
        {
            var photosList = new List<GetAllUserPhotosDto>();

            var photos = await _dataContext.UserPhotos.Where(x => x.UserId == userId).ToListAsync();

            foreach (var photo in photos)
            {
                photosList.Add(new GetAllUserPhotosDto
                {
                    Url = photo.Url,
                    Body = photo.Body,
                    LikesCount = photo.LikesCount
                });
            }

            if (!photosList.Any())
            {
                return null;
            }

            return photosList;
        }

        public async Task<GetMainUserPhotoDto> GetMainUserPhoto(Guid userId)
        {
            var photo = await _dataContext.UserPhotos.Where(x => x.UserId == userId && x.IsMain == true).ToListAsync();

            if (!photo.Any())
            {
                return null;
            }

            return new GetMainUserPhotoDto
            {
                Url = photo[0].Url,
                Body = photo[0].Body
            };
        }

        public async Task<GetSelectedUserPhotoDto> GetSelectedUserPhoto(Guid userId, string photoId)
        {
            var photo = await _dataContext.UserPhotos.Where(x => x.UserId == userId && x.Id == photoId).ToListAsync();

            if (!photo.Any())
            {
                return null;
            }

            return new GetSelectedUserPhotoDto
            {
                Url = photo[0].Url,
                Body = photo[0].Body
            };
        }
    }
}
