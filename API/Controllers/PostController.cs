using API.Dtos;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.LikesDto;

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
        public async Task<IActionResult> AddPost([FromForm] List<IFormFile> files, [FromForm] AddPostDto post)
        {
            await _postService.AddPost(files, post);

            return Ok();
        }

        [HttpGet("post/{id}")]
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePost(string id)
        {
            await _postService.DeletePostById(id);

            return Ok();
        }

        [HttpPut("update/{postId}")]
        public async Task<IActionResult> UpdatePost(Guid postId, EditPostDto newPost)
        {
            await _postService.UpdatePostById(postId, newPost);

            return Ok();
        }
    }
}
