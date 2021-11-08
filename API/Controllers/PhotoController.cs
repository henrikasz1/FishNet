using API.Dtos.Responses;
using API.Services;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly DataContext _dataContext;//
        private readonly IUserAccessorService _userAccessorService;//

        public PhotoController(IPhotoService photoService, DataContext dataContext, IUserAccessorService userAccessorService)
        {
            _photoService = photoService;
            _dataContext = dataContext;//
            _userAccessorService = userAccessorService;//
        }

        [HttpPost("User")]
        public async Task<ActionResult<PhotoUploadResponse>> UploadUserPhoto(IFormFile file)
        {
            var result = await _photoService.SaveUserPhoto(file);

            if(result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

    }
}
