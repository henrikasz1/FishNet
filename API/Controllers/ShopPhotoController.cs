using API.Dtos.PhotoDtos;
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
    public class ShopPhotoController : ControllerBase
    {
        private readonly IShopPhotoService _shopPhotoService;

        public ShopPhotoController(IShopPhotoService shopPhotoService)
        {
            _shopPhotoService = shopPhotoService;
        }

        [HttpPost("upload/{shopId}")]
        public async Task<IActionResult> AddShopPhoto(IFormFile file, Guid shopId)
        {
            await _shopPhotoService.SaveShopPhoto(file, shopId);

            return Ok();
        }

        [HttpDelete("delete/{photoId}")]
        public async Task<IActionResult> DeleteShopPhoto(string photoId)
        {
            await _shopPhotoService.DeleteShopPhotoById(photoId);

            return Ok();
        }

        [HttpPut("changemainshopphoto/{photoId}")]
        public async Task<IActionResult> ChangeMainShopPhoto(string photoId)
        {
            await _shopPhotoService.ChangeMainShopPhoto(photoId);

            return Ok();
        }

        [HttpGet("getmainshopphoto/{shopId}")]
        public async Task<ActionResult<GetUserPhotoDto>> GetMainShopPhoto(Guid shopId)
        {
            var result = await _shopPhotoService.GetMainShopPhoto(shopId);

            return Ok(result);
        }
    }
}
