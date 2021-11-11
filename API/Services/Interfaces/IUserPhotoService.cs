using API.Dtos.Responses;
using API.Dtos.UserPhotoDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IUserPhotoService
    {
        Task<PhotoUploadResponse> SaveUserPhoto(IFormFile file, string body);
        Task DeleteUserPhoto(string photoId);
        Task<IList<GetAllUserPhotosDto>> GetAllUserPhotos(Guid userId);
        Task<GetMainUserPhotoDto> GetMainUserPhoto(Guid userId);
        Task<GetSelectedUserPhotoDto> GetSelectedUserPhoto(Guid userId, string photoId);
    }
}
