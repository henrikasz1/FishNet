using API.Dtos.EventDtos;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class OccasionController : ControllerBase
    {
        private readonly IOccasionService _occasionService;

        public OccasionController(IOccasionService occasionService)
        {
            _occasionService = occasionService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddOccasion([FromForm]IFormFile file, [FromForm]AddOccasionDto occasionDto) 
        {
            await _occasionService.AddOccasion(file, occasionDto);

            return Ok();
        }

        [HttpDelete("delete/{occasionId}")]
        public async Task<IActionResult> DeleteOccasionByOccasionId(Guid occasionId)
        {
            await _occasionService.DeleteOccasionById(occasionId);

            return Ok();
        }

        [HttpGet("{occasionId}")]
        public async Task<ActionResult<GetOccasionDto>> GetOccasionByOccasionId(Guid occasionId)
        {
            var result = await _occasionService.GetOccasionById(occasionId);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetOccasionDto>> GetAllOccasions()
        {
            var result = await _occasionService.GetAllOccasions();

            return Ok(result);
        }

        [HttpGet("userOccasions/{hostId}")]
        public async Task<ActionResult<GetOccasionDto>> GetOccasionsByHostId(Guid hostId)
        {
            var result = await _occasionService.GetOccasionsByHostId(hostId);

            return Ok(result);
        }

        [HttpPut("edit/{occasionId}")]
        public async Task<IActionResult> EditOccasionByOccasionId(Guid occasionId, [FromForm] EditOccasionDto newOccasion,
            [FromForm]IFormFile file)
        {
            await _occasionService.EditOccasionByOccasionId(occasionId, newOccasion);

            return Ok();
        }
    }
}