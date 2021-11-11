using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesService;
        private readonly IUserAccessorService _userAccessor;
        public LikesController(ILikesService likesService, IUserAccessorService userAccessor)
        {
            _likesService = likesService;
            _userAccessor = userAccessor;
        }

        [HttpPost("likepost/{postId}")]
        public async Task<IActionResult> LikePost(Guid postId)
        {
            var userId = _userAccessor.GetCurrentUserId();

            await _likesService.LikePost(postId, Guid.Parse(userId));

            return Ok();
        }

        [HttpPost("unlikepost/{postId}")]
        public async Task<IActionResult> UnlikePost(Guid postId)
        {
            var userId = _userAccessor.GetCurrentUserId();

            await _likesService.UnlikePost(postId, Guid.Parse(userId));

            return Ok();
        }

        [HttpPost("likephoto/{photoId}")]
        public async Task<IActionResult> LikeUserPhoto(string photoId)
        {
            var userId = _userAccessor.GetCurrentUserId();

            await _likesService.LikeUserPhoto(photoId, Guid.Parse(userId));

            return Ok();
        }

        [HttpPost("unlikephoto/{photoId}")]
        public async Task<IActionResult> UnlikeUserPhoto(string photoId)
        {
            var userId = _userAccessor.GetCurrentUserId();

            await _likesService.UnlikePhoto(photoId, Guid.Parse(userId));

            return Ok();
        }
    }
}
