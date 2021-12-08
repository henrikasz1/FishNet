using API.Dtos.EventDtos;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class OccasionController : ControllerBase
    {
        private readonly IOccasionService _occasionService;

        public OccasionController(IOccasionService eventService)
        {
            _occasionService = eventService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddOccasion(AddOccasionDto occasionDto) 
        {
            await _occasionService.AddOccasion(occasionDto);

            return Ok();
        }

        [HttpDelete("delete/{occasionId}")]
        public async Task<IActionResult> DeleteOccasionByOccasionId(Guid occasionId)
        {
            await _occasionService.DeleteOccasionById(occasionId);

            return Ok();
        }

        [HttpGet("occasion/{occasionId}")]
        public async Task<ActionResult<GetOccasionDto>> GetOccasionByOccasionId(Guid occasionId)
        {
            var result = await _occasionService.GetOccasionById(occasionId);

            return Ok(result);
        }
    }
}