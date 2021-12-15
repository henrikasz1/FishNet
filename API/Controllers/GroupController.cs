using System;
using System.Threading.Tasks;
using API.Dtos.EventDtos;
using API.Dtos.GroupDtos;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddGroup([FromForm]IFormFile file, [FromForm]AddGroupDto groupDto) 
        {
            await _groupService.AddGroup(file, groupDto);

            return Ok();
        }
        
        [HttpPost("join/{groupId}")]
        public async Task<IActionResult> JoinGroup(Guid groupId)
        {
            await _groupService.JoinGroup(groupId);

            return Ok();
        }

        [HttpDelete("leave/{groupId}")]
        public async Task<IActionResult> LeaveGroup(Guid groupId)
        {
            await _groupService.LeaveGroup(groupId);

            return Ok();
        }
        
        [HttpPost("add/{groupId}/{userId}")]
        public async Task<IActionResult> AddMember(Guid groupId, Guid userId)
        {
            await _groupService.AddMember(groupId, userId);

            return Ok();
        }

        [HttpDelete("remove/{groupId}/{userId}")]
        public async Task<IActionResult> RemoveMember(Guid groupId, Guid userId)
        {
            await _groupService.RemoveMember(groupId, userId);

            return Ok();
        }
        
        [HttpGet("{groupId}")]
        public async Task<ActionResult<GetOccasionDto>> GetGroupById(Guid groupId)
        {
            var result = await _groupService.GetGroupById(groupId);

            return Ok(result);
        }
        [HttpGet("byOwnerId/{ownerId}")]
        public async Task<ActionResult<GetOccasionDto>> GetGroupByOwnerId(Guid ownerId)
        {
            var result = await _groupService.GetGroupByOwnerId(ownerId);

            return Ok(result);
        }
        
        [HttpGet]
        public async Task<ActionResult<GetOccasionDto>> GetAllGroups()
        {
            var result = await _groupService.GetAllGroups();

            return Ok(result);
        }
        
        [HttpDelete("delete/{groupId}")]
        public async Task<IActionResult> DeleteGroupById(Guid groupId)
        {
            await _groupService.DeleteGroupById(groupId);

            return Ok();
        }
        
        [HttpPut("edit/{groupId}")]
        public async Task<IActionResult> EditGroupById(Guid groupId, EditGroupDto newGroup)
        {
            await _groupService.EditGroup(groupId, newGroup);

            return Ok();
        }

        [HttpPut("{groupId}/giveowner/{userId}")]
        public async Task<IActionResult> GiveOwnership(Guid groupId, Guid userId, EditOwnershipDto newOwner)
        {
            await _groupService.GiveOwnership(groupId, userId, newOwner);

            return Ok();
        }
    }
}