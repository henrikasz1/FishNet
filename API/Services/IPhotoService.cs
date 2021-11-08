using API.Dtos.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPhotoService
    {
        Task<PhotoUploadResponse> SaveUserPhoto(IFormFile file);
        Task SavePostPhoto(IFormFile file, Guid postId);
    }
}
