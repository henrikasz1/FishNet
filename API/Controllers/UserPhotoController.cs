using API.Dtos.Responses;
using API.Services;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserPhotoController : ControllerBase
    {
        private readonly IUserPhotoService _userPhotoService;

        public UserPhotoController(IPostPhotoService photoService, DataContext dataContext, IUserPhotoService userPhotoService)
        {
            _userPhotoService = userPhotoService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<PhotoUploadResponse>> UploadUserPhoto(IFormFile file)
        {
            var result = await _userPhotoService.SaveUserPhoto(file);

            if(result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("delete/{photoId}")]
        public async Task<IActionResult> DeleteUserPhoto(string photoId)
        {
            await _userPhotoService.DeleteUserPhoto(photoId);

            return Ok();
        }
    }
}
