using API.Dtos.PhotoDtos;
using API.Dtos.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.LikesDto;

namespace API.Services.Interfaces
{
    public interface IUserPhotoService
    {
        Task<PhotoUploadResponse> SaveUserPhoto(IFormFile file, string body);
        Task DeleteUserPhoto(string photoId);
        Task<IList<GetUserPhotoDto>> GetAllUserPhotos(Guid userId);
        Task<GetUserPhotoDto> GetMainUserPhoto(Guid userId);
        Task<GetUserPhotoDto> GetSelectedUserPhoto(Guid userId, string photoId);
        Task<string> ChangeMainUserPhoto(string newMainPhotoId);
        Task<IList<GetLikesDto>> GetPhotoLikesById(string photoId);
    }
}
