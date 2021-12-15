using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.LikesDto;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesService;
        private readonly IUserAccessorService _userAccessor;
        private readonly IPostService _postService;
        private readonly IUserPhotoService _userPhotoService;
        private readonly ICommentService _commentService;
        public LikesController(ILikesService likesService, IUserAccessorService userAccessor,
            IPostService postService, IUserPhotoService userPhotoService, ICommentService commentService)
        {
            _likesService = likesService;
            _postService = postService;
            _userAccessor = userAccessor;
            _userPhotoService = userPhotoService;
            _commentService = commentService;
        }

        [HttpPost("likepost/{postId}")]
        public async Task<IActionResult> LikePost(Guid postId)
        {
            await _likesService.LikePost(postId);

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

        [HttpPost("likecomment/{commentId}")]
        public async Task<IActionResult> LikeComment(Guid commentId)
        {
            await _likesService.LikeComment(commentId);

            return Ok();
        }

        [HttpPost("unlikecomment/{commentId}")]
        public async Task<IActionResult> UnlikeComment(Guid commentId)
        {
            await _likesService.UnlikeComment(commentId);

            return Ok();
        }
        
        [HttpGet("postlikedby/{postId}")]
        public async Task<ActionResult<IList<GetLikesDto>>> GetPostLikesById(Guid postId)
        {
            var result = await _postService.GetPostLikesById(postId);

            return Ok(result);
        }

        [HttpGet("photolikedby/{photoId}")]
        public async Task<ActionResult<IList<GetLikesDto>>> GetPhotoLikesById(string photoId)
        {
            var result = await _userPhotoService.GetPhotoLikesById(photoId);
            
            return Ok(result);
        }

        [HttpGet("commentlikedby/{commentId}")]
        public async Task<ActionResult<IList<GetLikesDto>>> GetCommentLikesById(Guid commentId)
        {
            var result = await _commentService.GetCommentLikesById(commentId);

            return Ok(result);
        }
    }
}
