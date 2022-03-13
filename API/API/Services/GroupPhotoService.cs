using API.Infrastracture;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public class GroupPhotoService : IGroupPhotoService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IPhotoAccessor _photoAccessor;

        public GroupPhotoService(
            DataContext dataContext,
            IUserAccessorService userAccessorService,
            IPhotoAccessor photoAccessor)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
            _photoAccessor = photoAccessor;
        }
        
        public async Task SaveGroupPhoto(IFormFile file, Guid groupId)
        {
            var group = await _dataContext.Groups.Include(x => x.Photo)
                .FirstOrDefaultAsync(y => y.GroupId == groupId);

            if (group == null) throw new Exception("Invalid group");
            
            if (group.OwnerId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Unauthorized to add a photo");
            }
            
            if (group.Photo != null) throw new Exception("You can not add more than one photo");

            var photoUploadResult = await _photoAccessor.AddPhoto(file);
            
            var photo = new GroupPhoto
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId,
                GroupId = group.GroupId,
            };
            
            _dataContext.GroupPhoto.Add(photo);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Failed to add");
            }
        }
        
        public async Task DeleteGroupPhotoById(string photoId)
        {
            var photo = await _dataContext.GroupPhoto.FindAsync(photoId);

            var group = await _dataContext.Groups.FirstOrDefaultAsync(x => x.GroupId == photo.GroupId);

            if (group.OwnerId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Unauthorized to delete this photo");
            }
            await _photoAccessor.DeletePhoto(photo.Id);
            
            _dataContext.GroupPhoto.Remove(photo);

            await _dataContext.SaveChangesAsync();
        }
    }
}