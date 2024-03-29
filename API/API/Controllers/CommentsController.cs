﻿using API.Dtos.CommentsDtos;
using API.Dtos.Responses;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("{postId}")]
        public async Task<ActionResult<CommentResponse>> AddComment(Guid postId, CommentDto comment)
        {
            var result = await _commentService.AddComment(postId, comment);

            return Ok(result);
        }

        

        

        [HttpDelete("delete/{commentId}")]
        public async Task<ActionResult<CommentResponse>> DeleteComment(Guid commentId)
        {
            var result = await _commentService.DeleteComment(commentId);

            return Ok(result);
        }
    }
}
