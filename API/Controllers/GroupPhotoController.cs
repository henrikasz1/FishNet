using System;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupPhotoController : ControllerBase
    {
        private readonly IGroupPhotoService _groupPhotoService;
        
        public GroupPhotoController(IGroupPhotoService groupPhotoService)
        {
            _groupPhotoService = groupPhotoService;
        }
        
        [HttpPost("upload/{groupId}")]
        public async Task<IActionResult> AddGroupPhoto(IFormFile file, Guid groupId)
        {
            await _groupPhotoService.SaveGroupPhoto(file, groupId);

            return Ok();
        }
        
        [HttpDelete("delete/{photoId}")]
        public async Task<IActionResult> DeleteGroupPhotoById(string photoId)
        {
            await _groupPhotoService.DeleteGroupPhotoById(photoId);

            return Ok();
        }
    }
}