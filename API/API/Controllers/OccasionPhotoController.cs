using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OccasionPhotoController : ControllerBase
    {
        private readonly IOccasionPhotoService _occasionPhotoService;
        public OccasionPhotoController(IOccasionPhotoService occasionPhotoService)
        {
            _occasionPhotoService = occasionPhotoService;
        }
        
        [HttpPost("upload/{occasionId}")]
        public async Task<IActionResult> AddOccasionPhoto(IFormFile file, Guid occasionId)
        {
            await _occasionPhotoService.SaveOccasionPhoto(file, occasionId);

            return Ok();
        }
        
        [HttpDelete("delete/{photoId}")]
        public async Task<IActionResult> DeleteOccasionPhotoById(string photoId)
        {
            await _occasionPhotoService.DeleteOccasionPhotoById(photoId);

            return Ok();
        }

        [HttpPut("makemain/{photoId}")]
        public async Task<IActionResult> MakeOccasionPhotoMainById(string photoId)
        {
            await _occasionPhotoService.MakeOccasionPhotoMainById(photoId);

            return Ok();
        }
    }
}