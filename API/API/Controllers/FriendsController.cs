using API.Dtos.FriendsDtos;
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
    public class FriendsController : ControllerBase
    {
        private readonly IFriendsService _friendsService;

        public FriendsController(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        [HttpPost("request/{userId}")]
        public async Task<ActionResult<FriendRequestResponse>> SendFriendRequest(Guid userId)
        {
            var result = await _friendsService.SendFriendRequest(userId);

            return Ok(result);
        }

        [HttpPut("approve/{friendId}")]
        public async Task<ActionResult<FriendRequestResponse>> ApproveFriendRequest(Guid friendId)
        {
            var result = await _friendsService.ApproveFriendRequest(friendId);

            return Ok(result);
        }

        [HttpDelete("decline/{friendId}")]
        public async Task<ActionResult<FriendRequestResponse>> DeclineFriendRequest(Guid friendId)
        {
            var result = await _friendsService.DeclineFriendRequest(friendId);

            return Ok(result);
        }

        [HttpGet("list")]
        public async Task<ActionResult<IList<GetFriendsDto>>> GetFriendsList()
        {
            var result = await _friendsService.GetAllFriendsList();
            
            return Ok(result);
        }

        [HttpDelete("unfriend/{friendId}")]
        public async Task<ActionResult<FriendRequestResponse>> Unfriend(Guid friendId)
        {
            var result = await _friendsService.Unfriend(friendId);

            return Ok(result);
        }
    }
}
