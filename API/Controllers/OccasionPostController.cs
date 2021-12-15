using API.Dtos;
using API.Services.Interfaces;
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
    public class OccasionPostController : ControllerBase
    {
        private readonly IOccasionPostService _occasionPostService;

        public OccasionPostController(IOccasionPostService occasionPostService)
        {
            _occasionPostService = occasionPostService;
        }

        [HttpPost("{occasionId}")]
        public async Task<IActionResult> AddOccasionPost(Guid occasionId, [FromForm] List<IFormFile> files, [FromForm] AddPostDto post)
        {
            await _occasionPostService.AddOccasionPost(files, occasionId, post);

            return Ok();
        }

        [HttpDelete("delete/{postId}")]
        public async Task<IActionResult> DeleteOccasionPost(Guid postId)
        {
            await _occasionPostService.DeleteOccasionPost(postId);

            return Ok();
        }

        [HttpGet("occasionposts/{occasionId}")]
        public async Task<ActionResult<IList<GetPostDto>>> GetAllOccasionPosts(Guid occasionId)
        {
            var result = await _occasionPostService.GetAllOccasionPosts(occasionId);

            return Ok(result);
        }
    }
}
