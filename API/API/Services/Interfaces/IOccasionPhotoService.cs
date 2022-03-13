using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Services.Interfaces
{
    public interface IOccasionPhotoService
    {
        Task SaveOccasionPhoto(IFormFile file, Guid occasionId);
        Task DeleteOccasionPhotoById(string photoId);
        Task MakeOccasionPhotoMainById(string photoId);
    }
}