using API.Dtos;
using API.Services;
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
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(
            IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromForm] IFormFile file, [FromForm] AddPostDto post)
        {
            await _postService.AddPost(file, post);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPostDto>> GetPostByPostId(Guid id)
        {
            var result = await _postService.GetPostById(id);

            return Ok(result);
        }

        [HttpGet("userposts/{userId}")]
        public async Task<ActionResult<IList<GetPostDto>>> GetUsersPosts(Guid userId)
        {
            var result = await _postService.GetPostsByUserId(userId);

            return Ok(result);
        }

        [HttpGet("allposts")]
        public async Task<ActionResult<IList<GetPostDto>>> GetAllPosts()
        {
            var result = await _postService.GetAllPosts();

            return Ok(result);
        }
    }
}
