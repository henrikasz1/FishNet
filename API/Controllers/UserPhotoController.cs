using API.Dtos.Responses;
using API.Dtos.UserPhotoDtos;
using API.Services;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<PhotoUploadResponse>> UploadUserPhoto(IFormFile file, [FromForm]string body)
        {
            var result = await _userPhotoService.SaveUserPhoto(file, body);

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

        [HttpGet("getall/{userId}")]
        public async Task<ActionResult<IList<GetAllUserPhotosDto>>> GetAllUserPhotos(Guid userId)
        {
            var result = await _userPhotoService.GetAllUserPhotos(userId);

            if (result == null)
            {
                return Ok("No photos to show");
            }

            return Ok(result);
        }

        [HttpGet("getmainphoto/{userId}")]
        public async Task<ActionResult<GetMainUserPhotoDto>> GetUserMainPhoto(Guid userId)
        {
            var result = await _userPhotoService.GetMainUserPhoto(userId);

            if (result == null)
            {
                return Ok("User does not have main photo");
            }

            return Ok(result);
        }

        [HttpGet("getselectedphoto/{userId}/{photoId}")]
        public async Task<ActionResult<GetSelectedUserPhotoDto>> GetSelectedUserPhoto(Guid userId, string photoId)
        {
            var result = await _userPhotoService.GetSelectedUserPhoto(userId, photoId);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
