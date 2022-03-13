using API.Dtos.PhotoDtos;
using API.Dtos.Responses;
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
using API.Dtos.LikesDto;

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
        public async Task<IList<GetLikesDto>> GetPhotoLikesById(string photoId)
        {
            var usersDtoList = new List<GetLikesDto>();

            var likes = await _dataContext.PhotoLikes
                .Where(x => x.ObjectId == photoId)
                .ToListAsync();

            foreach (var like in likes)
            {
                var user = await _dataContext.Users
                    .Include(x => x.Photos)
                    .FirstOrDefaultAsync(x => x.UserId == like.LoverId);
                var userMainPhoto = user.Photos.Any() ? user.Photos
                    .FirstOrDefault(x => x.IsMain == true)
                    .Url : string.Empty;
                
                usersDtoList.Add(
                    new GetLikesDto()
                    {
                        UserId = like.LoverId,
                        MainPhotoUrl = userMainPhoto,
                        FirstName= user.FirstName,
                        LastName = user.LastName
                    });
            }
            return usersDtoList;
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

        public async Task<IList<GetUserPhotoDto>> GetAllUserPhotos(Guid userId)
        {
            var photosList = new List<GetUserPhotoDto>();

            var photos = await _dataContext.UserPhotos.Where(x => x.UserId == userId).ToListAsync();

            foreach (var photo in photos)
            {
                photosList.Add(new GetUserPhotoDto
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

        public async Task<GetUserPhotoDto> GetMainUserPhoto(Guid userId)
        {
            var photo = await _dataContext.UserPhotos.Where(x => x.UserId == userId && x.IsMain == true).ToListAsync();

            if (!photo.Any())
            {
                return null;
            }

            return new GetUserPhotoDto
            {
                Url = photo[0].Url,
                Body = photo[0].Body
            };
        }

        public async Task<string> ChangeMainUserPhoto(string newMainPhotoId)
        {
            var photo = await _dataContext.UserPhotos.FindAsync(newMainPhotoId);
            
            var user = await _dataContext.Users
                .FirstOrDefaultAsync(x => x.UserId == photo.UserId);

            if (user.UserId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Unauthorized to select this photo as main");
            }

            var mainPhoto = await _dataContext.UserPhotos
                .Where(x => x.IsMain == true)
                .FirstOrDefaultAsync();

            mainPhoto.IsMain = false;
            photo.IsMain = true;

            var result = await _dataContext.SaveChangesAsync() > 0;

            return !result
                ? throw new DbUpdateException("Failed to make the photo as main")
                : "Photo has been changed to main successfully";
        }

        public async Task<GetUserPhotoDto> GetSelectedUserPhoto(Guid userId, string photoId)
        {
            var photo = await _dataContext.UserPhotos.Where(x => x.UserId == userId && x.Id == photoId).ToListAsync();

            if (!photo.Any())
            {
                return null;
            }

            return new GetUserPhotoDto
            {
                Url = photo[0].Url,
                Body = photo[0].Body
            };
        }
    }
}
