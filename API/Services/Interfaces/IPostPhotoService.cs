using API.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPostPhotoService
    {
        Task SavePostPhoto(IFormFile file, Guid postId);
        Task DeletePostPhoto(PostPhoto photo);
        Task DeletePostPhotoById(string photoId);
        Task<string> ChangeMainPostPhoto(string newMainPhotoId);
    }
}
