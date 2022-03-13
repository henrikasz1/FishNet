using API.Dtos.Responses;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace API.Infrastracture
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResponse> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string publicId);
    }
}