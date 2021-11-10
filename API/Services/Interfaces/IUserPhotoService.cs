using API.Dtos.Responses;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IUserPhotoService
    {
        Task<PhotoUploadResponse> SaveUserPhoto(IFormFile file);
        Task DeleteUserPhoto(string photoId);
    }
}
