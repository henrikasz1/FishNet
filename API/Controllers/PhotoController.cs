using API.Models;
using API.Services;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            var result = await _photoService.SavePhoto(file);

            if(result == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        //Test endpoint
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            //var us = await _dataContext.Users.SingleOrDefaultAsync(
               //x => x.UserId == Guid.Parse(_userAccessorService.GetCurrentUserId()));

            var user = await _dataContext.Users.Include(x => x.Photos)
              .FirstOrDefaultAsync(y => y.UserId == Guid.Parse(_userAccessorService.GetCurrentUserId()));

            if (id != user.UserId)
            {
                return Unauthorized();
            }

            return Ok(user.Photos);
        }

    }
}
