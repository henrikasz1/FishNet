using API.Services;
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
    public class PostPhotoController : ControllerBase
    {
        private readonly IPostPhotoService _postPhotoService;
        public PostPhotoController(IPostPhotoService postPhotoService)
        {
            _postPhotoService = postPhotoService;
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> AddPostPhoto(IFormFile file, Guid postId)
        {
            await _postPhotoService.SavePostPhoto(file, postId);

            return Ok();
        }

        [HttpDelete()]
        [Route("delete/{photoId}")]
        public async Task<IActionResult> DeletePostPhoto(string photoId)
        {
            await _postPhotoService.DeletePostPhotoById(photoId);

            return Ok();
        }

        [HttpPut("changemainpostphoto/{photoId}")]
        public async Task<ActionResult<string>> ChangeMainPostPhoto(string photoId)
        {
            var result = await _postPhotoService.ChangeMainPostPhoto(photoId);

            return Ok(result);
        }
    }
}
