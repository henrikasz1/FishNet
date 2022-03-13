using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Services.Interfaces
{
    public interface IGroupPhotoService
    {
        Task SaveGroupPhoto(IFormFile file, Guid groupId);
        Task DeleteGroupPhotoById(string photoId);
    }
}