using API.Dtos.Responses;
using API.Dtos.ShopDtos;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static API.Models.Enums.ShopEnum;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IUserAccessorService _userAccessor;

        public ShopController(IShopService shopService, IUserAccessorService userAccessorService)
        {
            _shopService = shopService;
            _userAccessor = userAccessorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddShopAd([FromForm] List<IFormFile> files, [FromForm] AddShopAdDto shopAd)
        {
            await _shopService.AddShopAd(files, shopAd);

            return Ok();
        }

        [HttpGet("shop/{id}")]
        public async Task<ActionResult<GetShopAdDto>> GetShopAdById(Guid id)
        {
            var result = await _shopService.GetShopAdById(id);

            return Ok(result);
        }

        [HttpGet("usershopads/{userId}")]
        public async Task<ActionResult<IList<GetShopAdDto>>> GetShopAdsByUserId(Guid userId)
        {
            var result = await _shopService.GetShopAdsByUserId(userId);

            return Ok(result);
        }

        [HttpGet("getbycategory/{category}")]
        public async Task<ActionResult<List<GetShopAdDto>>> GetShopAdsByCategory(ProductType category)
        {
            var result = await _shopService.GetShopAdByCategory(category);

            return Ok(result);
        }

        [HttpGet("allshopads")]
        public async Task<ActionResult<IList<GetShopAdDto>>> GetAllShopAds()
        {
            var result = await _shopService.GetAllShopAds();

            return Ok(result);
        }

        [HttpGet("shopbyname/{filter}")]
        public async Task<ActionResult<IList<GetShopAdDto>>> GetShopAdsByName(string filter)
        {
            var result = await _shopService.GetShopAdsByName(filter);

            return Ok(result);
        }

        [HttpDelete("delete/{shopId}")]
        public async Task<ActionResult<string>> DeleteShopAd(Guid shopId)
        {
            var result = await _shopService.DeleteShopAdById(shopId);

            return Ok(result);
        }

        [HttpPut("update/{shopId}")]
        public async Task<IActionResult> UpdateShop(Guid shopId, [FromForm] EditShopAdDto newShopAd)
        {
            await _shopService.UpdateShopAdById(shopId, newShopAd);

            return Ok();
        }
    }
}
