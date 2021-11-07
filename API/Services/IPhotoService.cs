using API.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPhotoService
    {
        Task<Photo> SavePhoto(IFormFile file);
    }
}
