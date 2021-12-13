using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.GroupDtos;
using API.Dtos.SearchDtos;
using API.Models;
using API.Models.Enums;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class GroupService : IGroupService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IGroupPhotoService _groupPhotoService;

        public GroupService(
            DataContext dataContext,
            IUserAccessorService userAccessorService,
            IGroupPhotoService groupPhotoService)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
            _groupPhotoService = groupPhotoService;
        }
        
        public async Task AddGroup(IFormFile file, AddGroupDto group)
        {
            var ownerId = _userAccessorService.GetCurrentUserId();

            var newGroup = new Group()
            {
                GroupId = Guid.NewGuid(),
                OwnerId = Guid.Parse(ownerId),
                Title = group.Title,
                Description = group.Description,
            };
            
            newGroup.Members = new List<GroupUser>();
            newGroup.Members.Add(new GroupUser {UserId = Guid.Parse(ownerId),
                GroupId = newGroup.GroupId});
            newGroup.MembersCount++;
            _dataContext.Groups.Add(newGroup);
            var result = await _dataContext.SaveChangesAsync() > 0;

            await _groupPhotoService.SaveGroupPhoto(file, newGroup.GroupId);

            if (!result)
            {
                throw new DbUpdateException("Failed to create a group");
            }
        }
        
        public async Task JoinGroup(Guid groupId)
        {
            var userId = _userAccessorService.GetCurrentUserId();
            var group = await _dataContext.Groups
                .Include(y => y.Members)
                .FirstOrDefaultAsync(y => y.GroupId == groupId);

            if (group == null) throw new Exception("Invalid group");
            if (group.Members.Any(x => Equals(userId)))
            {
                throw new Exception("User has already joined the group");
            }
            
            var member = new GroupUser()
            {
                UserId = Guid.Parse(userId),
                GroupId = group.GroupId,
            };
            
            group.MembersCount++;
            _dataContext.GroupUsers.Add(member);
            
            var result = await _dataContext.SaveChangesAsync() > 0;
            
            if (!result)
            {
                throw new DbUpdateException("Failed to join an occasion");
            }
        }

        public async Task LeaveGroup(Guid groupId)
        {
            var userId = _userAccessorService.GetCurrentUserId();
            var user = await _dataContext.GroupUsers
                .FirstOrDefaultAsync(y => y.UserId == Guid.Parse(userId));
            var group = await _dataContext.Groups
                .Include(y => y.Members)
                .FirstOrDefaultAsync(y => y.GroupId == groupId);

            if (group == null) throw new Exception("Invalid occasion");
            
            if (group.Members.All(x => x.UserId != Guid.Parse(userId)))
            {
                throw new Exception("User is not a member of the group");
            }

            if (group.OwnerId != Guid.Parse(userId))
            {
                throw new Exception("As an group owner, you cannot leave the group");
            }
            group.MembersCount--;
            _dataContext.GroupUsers.Remove(user);
            
            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Unable to leave the group");
            }
        }

        public async Task AddMember(Guid groupId, Guid userId)
        {
            var memberId = _userAccessorService.GetCurrentUserId();
            var group = await _dataContext.Groups
                .Include(y => y.Members)
                .FirstOrDefaultAsync(y => y.GroupId == groupId);

            if (group == null)
            {
                throw new Exception("Invalid group");
            }

            if (group.Members.Any(x => Equals(userId)))
            {
                throw new Exception("User is already in the group");
            }

            if (Guid.Parse(memberId) == userId)
            {
                throw new Exception("You can not add yourself to the group");
            }

            if (group.OwnerId != Guid.Parse(memberId))
            {
                throw new Exception("Only group owner can add another people to the group");
            }

            var member = new GroupUser()
            {
                UserId = userId,
                GroupId = group.GroupId,
            };
            
            group.MembersCount++;
            _dataContext.GroupUsers.Add(member);
            
            var result = await _dataContext.SaveChangesAsync() > 0;
            
            if (!result)
            {
                throw new DbUpdateException("Failed to add a member");
            }
            
        }
        
        public async Task RemoveMember(Guid groupId, Guid userId)
        {
            var memberId = _userAccessorService.GetCurrentUserId();
            var group = await _dataContext.Groups
                .Include(y => y.Members)
                .FirstOrDefaultAsync(y => y.GroupId == groupId);
            var user = await _dataContext.GroupUsers
                .FirstOrDefaultAsync(y => y.UserId == userId);
            
            if (group == null)
            {
                throw new Exception("Invalid group");
            }

            if (group.Members.All(x => x.UserId != userId))
            {
                throw new Exception("User is not a member of the group");
            }

            if (Guid.Parse(memberId) == userId)
            {
                throw new Exception("You can not remove yourself from the group");
            }

            if (group.OwnerId != Guid.Parse(memberId))
            {
                throw new Exception("Only group owner can remove another people from the group");
            }

            group.MembersCount--;
            _dataContext.GroupUsers.Remove(user);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Unable to remove the user");
            }
        }
        public async Task<GetGroupDto> GetGroupById(Guid groupId)
        {
            var group = await _dataContext.Groups
                .Include(y => y.Members)
                .Include(y => y.Photo)
                .FirstOrDefaultAsync(y => y.GroupId == groupId);

            var getGroup = new GetGroupDto
            {
                GroupId = group.GroupId, 
                OwnerId = group.OwnerId,
                Title = group.Title,
                Description = group.Description,
                MembersCount = group.MembersCount,
                Photo = group.Photo,
            };
            getGroup.MembersIds = new List<Guid>();
            foreach (var member in group.Members)
            {
                getGroup.MembersIds.Add(member.UserId);
            }
            return getGroup;
        }
        
        public async Task<List<GetSearchResultsDto>> GetGroupByName(string filter)
        {
            var groupsList = new List<GetSearchResultsDto>();
            var groups = await _dataContext.Groups
                .Include(y => y.Photo)
                .Where(y => y.Title.ToLower().Contains(filter.ToLower()))
                .ToListAsync();
            
            foreach (var group in groups)
            {
                groupsList.Add( new GetSearchResultsDto
                {
                    EntityId = group.OwnerId,
                    EntityMainPhotoUrl = group.Photo.Url,
                    EntityName = group.Title,
                    EntityType = SearchResultEnum.SearchResultType.Group
                });
            }
            return groupsList;
        }
        
        public async Task<IList<GetGroupDto>> GetAllGroups()
        {
            var groupsList = new List<GetGroupDto>();
            
            var groups = await _dataContext.Groups
                .Select(y => y)
                .Include(y => y.Members)
                .Include(y => y.Photo)
                .ToListAsync();

            foreach (var group in groups)
            {
                var getGroup = new GetGroupDto
                {
                    GroupId = group.GroupId,
                    OwnerId = group.OwnerId,
                    Title = group.Title,
                    Description = group.Description,
                    MembersCount = group.MembersCount,
                    Photo = group.Photo,
                };
                getGroup.MembersIds = new List<Guid>();
                foreach (var member in group.Members)
                {
                    getGroup.MembersIds.Add(member.UserId);
                }
                groupsList.Add(getGroup);
            }
            return groupsList;
        }

        public async Task<IList<GetGroupDto>> GetGroupByOwnerId(Guid ownerId)
        {
            var groupsList = new List<GetGroupDto>();
            
            var groups = await _dataContext.Groups
                .Where(y => y.OwnerId == ownerId)
                .Include(y => y.Members)
                .Include(y => y.Photo)
                .ToListAsync();

            foreach (var group in groups)
            {
                var getGroup = new GetGroupDto
                {
                    GroupId = group.GroupId,
                    OwnerId = group.OwnerId,
                    Title = group.Title,
                    Description = group.Description,
                    MembersCount = group.MembersCount,
                    Photo = group.Photo,
                };
                getGroup.MembersIds = new List<Guid>();
                foreach (var member in group.Members)
                {
                    getGroup.MembersIds.Add(member.UserId);
                }
                groupsList.Add(getGroup);
            }
            return groupsList;
        }
        public async Task DeleteGroupById(Guid groupId)
        {
            var group = await _dataContext.Groups.FindAsync(groupId);
            
            var userId = _userAccessorService.GetCurrentUserId();

            if (group.OwnerId != Guid.Parse(userId))
            {
                throw new UnauthorizedAccessException("Only the host can delete the occasion");
            }

            _dataContext.Groups.Remove(group);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Unable to remove group");
            }
        }
        
        public async Task EditGroup(Guid groupId, EditGroupDto newGroup)
        {
            var group = await _dataContext.Groups.FirstOrDefaultAsync(y => y.GroupId == groupId);

            if (group.OwnerId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Only group owner can update it");
            }
            
            group.Title = newGroup.Title;
            group.Description = newGroup.Description;

            var success = await _dataContext.SaveChangesAsync() > 0;

            if (!success)
            {
                throw new DbUpdateException("Could not update occasion");
            }
        }

        public async Task GiveOwnership(Guid groupId, Guid userId, EditOwnershipDto newOwner)
        {
            var group = await _dataContext.Groups
                .Include(y => y.Members)
                .FirstOrDefaultAsync(y => y.GroupId == groupId);
            var ownerId = group.OwnerId;

            if (group.Members.All(x => x.UserId != userId))
            {
                throw new Exception("Error giving ownership");
            }
            
            if (ownerId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new Exception("Only owner can give ownership to another member");
            }
            
            var oldOwner = await _dataContext.GroupUsers
                .FirstOrDefaultAsync(y => y.UserId == ownerId);

            group.OwnerId = newOwner.OwnerId;
            group.Members.Add(oldOwner);
        
            var success = await _dataContext.SaveChangesAsync() > 0;

            if (!success)
            {
                throw new DbUpdateException("Could not update the ownership");
            }
        }
    }
}