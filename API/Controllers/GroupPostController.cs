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
    public class GroupPostController : ControllerBase
    {
        private readonly IGroupPostService _groupPostService;

        public GroupPostController(IGroupPostService groupPostService)
        {
            _groupPostService = groupPostService;
        }

        [HttpPost("{groupId}")]
        public async Task<IActionResult> AddGroupPost(Guid groupId, [FromForm] List<IFormFile> files, [FromForm] AddPostDto post)
        {
            await _groupPostService.AddGroupPost(files, groupId, post);

            return Ok();
        }
    }
}
