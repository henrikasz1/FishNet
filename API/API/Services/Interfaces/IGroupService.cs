using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.GroupDtos;
using API.Dtos.SearchDtos;
using Microsoft.AspNetCore.Http;

namespace API.Services.Interfaces
{
    public interface IGroupService
    {
        Task AddGroup(IFormFile file, AddGroupDto group);
        Task JoinGroup(Guid groupId);
        Task LeaveGroup(Guid groupId);
        Task AddMember(Guid groupId, Guid userId);
        Task RemoveMember(Guid groupId, Guid userId);
        Task<GetGroupDto> GetGroupById(Guid groupId);
        Task<List<GetSearchResultsDto>> GetGroupByName(string filter);
        Task<IList<GetGroupDto>> GetGroupByOwnerId(Guid ownerId);
        Task<IList<GetGroupDto>> GetAllGroups();
        Task EditGroup(Guid groupId, EditGroupDto newGroup);
        Task DeleteGroupById(Guid groupId);
        Task GiveOwnership(Guid groupId, Guid userId, EditOwnershipDto newOwner);
        
    }
}