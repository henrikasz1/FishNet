using API.Dtos.EventDtos;
using API.Dtos.SearchDtos;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static API.Models.Enums.SearchResultEnum;

namespace API.Services
{
    public class OccasionService : IOccasionService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IOccasionPhotoService _occasionPhotoService;

        public OccasionService(
            DataContext dataContext,
            IUserAccessorService userAccessorService,
            IOccasionPhotoService occasionPhotoService)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
            _occasionPhotoService = occasionPhotoService;
        }

        public async Task AddOccasion(IFormFile file, AddOccasionDto occasion)
        {
            var hostId = _userAccessorService.GetCurrentUserId();

            var newOccasion = new Occasion()
            {
                OccasionId = Guid.NewGuid(),
                HostId = Guid.Parse(hostId),
                Title = occasion.Title,
                Description = occasion.Description,
                Location = occasion.Location,
                StartsAt = occasion.StartsAt,
                EndsAt = occasion.EndsAt,
            };

            if (newOccasion.StartsAt >= DateTime.Now && newOccasion.EndsAt >= newOccasion.StartsAt)
            {
                newOccasion.Participants = new List<OccasionUser>();
                newOccasion.Participants.Add(new OccasionUser {UserId = Guid.Parse(hostId),
                    OccasionId = newOccasion.OccasionId});
                newOccasion.ParticipantsCount++;
                _dataContext.Occasions.Add(newOccasion);
                var result = await _dataContext.SaveChangesAsync() > 0;

                await _occasionPhotoService.SaveOccasionPhoto(file, newOccasion.OccasionId);

                if (!result)
                {
                    throw new DbUpdateException("Failed to create an occasion");
                }
            }
            else
            {
                throw new Exception("Select a proper date");
            }
        }

        public async Task JoinOccasion(Guid occasionId)
        {
            var userId = _userAccessorService.GetCurrentUserId();
            var occasion = await _dataContext.Occasions
                .Include(y => y.Participants)
                .FirstOrDefaultAsync(y => y.OccasionId == occasionId);

            if (occasion == null) throw new Exception("Invalid occasion");
            if (occasion.Participants.Any(x => Equals(userId)))
            {
                throw new Exception("User has already joined the event");
            }
            
            var participant = new OccasionUser()
            {
                UserId = Guid.Parse(userId),
                OccasionId = occasion.OccasionId,
            };
            occasion.ParticipantsCount++;
            _dataContext.OccasionUsers.Add(participant);
            
            var result = await _dataContext.SaveChangesAsync() > 0;
            
            if (!result)
            {
                throw new DbUpdateException("Failed to join an occasion");
            }
        }

        public async Task LeaveOccasion(Guid occasionId)
        {
            var userId = _userAccessorService.GetCurrentUserId();
            var user = await _dataContext.OccasionUsers
                .FirstOrDefaultAsync(y => y.UserId == Guid.Parse(userId));
            var occasion = await _dataContext.Occasions
                .Include(y => y.Participants)
                .FirstOrDefaultAsync(y => y.OccasionId == occasionId);

            if (occasion == null) throw new Exception("Invalid occasion");
            
            if (occasion.Participants.All(x => x.UserId != Guid.Parse(userId)))
            {
                throw new Exception("User has not joined the event yet");
            }

            if (occasion.HostId != Guid.Parse(userId))
            {
                throw new Exception("As an occasion host, you cannot leave the occasion");
            }
            occasion.ParticipantsCount--;
            _dataContext.OccasionUsers.Remove(user);
            
            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Unable to leave the occasion");
            }
        }
        
        public async Task<GetOccasionDto> GetOccasionById(Guid occasionId)
        {
            var occasion = await _dataContext.Occasions
                .Include(y => y.Participants)
                .Include(y => y.Photos)
                .FirstOrDefaultAsync(y => y.OccasionId == occasionId);

            var newOccasion = new GetOccasionDto
            {
                OccasionId = occasion.OccasionId,
                HostId = occasion.HostId,
                Title = occasion.Title,
                Description = occasion.Description,
                Location = occasion.Location,
                StartsAt = occasion.StartsAt,
                EndsAt = occasion.EndsAt,
                ParticipantsCount = occasion.ParticipantsCount,
                Photos = occasion.Photos,
            };
            newOccasion.ParticipantsIds = new List<Guid>();
            foreach (var participant in occasion.Participants)
            {
                newOccasion.ParticipantsIds.Add(participant.UserId);
            }
            return newOccasion;
        }

        public async Task<List<GetSearchResultsDto>> GetOccasionByName(string filter)
        {
            var occasionsList = new List<GetSearchResultsDto>();
            var occasions = await _dataContext.Occasions
                .Include(y => y.Photos)
                .Where(y => y.Title.ToLower().Contains(filter.ToLower()))
                .ToListAsync();
            
            foreach (var occasion in occasions)
            {
                var occasionMainPhoto = occasion.Photos.Any() ? occasion.Photos
                    .FirstOrDefault(x => x.IsMain == true)
                    .Url : string.Empty;

                occasionsList.Add( new GetSearchResultsDto
                {
                    EntityId = occasion.OccasionId,
                    EntityMainPhotoUrl = occasionMainPhoto,
                    EntityName = occasion.Title,
                    EntityType = SearchResultType.Event
                });
            }
            return occasionsList;
        }

        public async Task<IList<GetOccasionDto>> GetAllOccasions()
        {
            var occasionsList = new List<GetOccasionDto>();
            
            var occasions = await _dataContext.Occasions
                .Select(y => y)
                .Include(y => y.Participants)
                .Include(y => y.Photos)
                .ToListAsync();

            foreach (var occasion in occasions)
            {
                var getOccasion = new GetOccasionDto
                {
                    OccasionId = occasion.OccasionId,
                    HostId = occasion.HostId,
                    Title = occasion.Title,
                    Description = occasion.Description,
                    Location = occasion.Location,
                    StartsAt = occasion.StartsAt,
                    EndsAt = occasion.EndsAt,
                    ParticipantsCount = occasion.ParticipantsCount,
                    Photos = occasion.Photos,
                };
                getOccasion.ParticipantsIds = new List<Guid>();
                foreach (var participant in occasion.Participants)
                {
                    getOccasion.ParticipantsIds.Add(participant.UserId);
                }
                occasionsList.Add(getOccasion);
            }
            return occasionsList;
        }

        public async Task<IList<GetOccasionDto>> GetOccasionsByHostId(Guid hostId)
        {
            var occasionsList = new List<GetOccasionDto>();

            var occasions = await _dataContext.Occasions
                .Where(y => y.HostId == hostId)
                .Include(y => y.Participants)
                .Include(y => y.Photos)
                .ToListAsync();

            foreach (var occasion in occasions)
            {
                var getOccasion = new GetOccasionDto
                {
                    OccasionId = occasion.OccasionId,
                    HostId = occasion.HostId,
                    Title = occasion.Title,
                    Description = occasion.Description,
                    Location = occasion.Location,
                    StartsAt = occasion.StartsAt,
                    EndsAt = occasion.EndsAt,
                    ParticipantsCount = occasion.ParticipantsCount,
                    Photos = occasion.Photos,
                };
                getOccasion.ParticipantsIds = new List<Guid>();
                foreach (var participant in occasion.Participants)
                {
                    getOccasion.ParticipantsIds.Add(participant.UserId);
                }
                occasionsList.Add(getOccasion);
            }
            return occasionsList;
        }

        public async Task DeleteOccasionById(Guid occasionId)
        {
            var occasion = await _dataContext.Occasions.FindAsync(occasionId);
            
            var hostId = _userAccessorService.GetCurrentUserId();

            if (occasion.HostId != Guid.Parse(hostId))
            {
                throw new UnauthorizedAccessException("Only the host can delete the occasion");
            }

            _dataContext.Occasions.Remove(occasion);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Unable to remove post");
            }
        }
        
        public async Task EditOccasionByOccasionId(Guid occasionId, EditOccasionDto newOccasion)
        {
            var occasion = await _dataContext.Occasions.FirstOrDefaultAsync(y => y.OccasionId == occasionId);

            if (occasion.HostId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Only occasion host can update it");
            }
            
            occasion.Title = newOccasion.Title;
            occasion.Description = newOccasion.Description;
            occasion.Location = newOccasion.Location;

            var oldStartDate = occasion.StartsAt;
            var oldEndDate = occasion.EndsAt;

            occasion.StartsAt = newOccasion.StartsAt;
            occasion.EndsAt = newOccasion.EndsAt;

            if (oldStartDate >= DateTime.Now && oldEndDate <= DateTime.Now)
            {
                if (occasion.StartsAt >= DateTime.Now && occasion.EndsAt >= newOccasion.StartsAt)
                {
                    var success = await _dataContext.SaveChangesAsync() > 0;

                    if (!success)
                    {
                        throw new DbUpdateException("Could not update occasion");
                    }
                }
                else
                {
                    throw new Exception("Select a proper date");
                }
            }
            else
            {
                throw new Exception("Occasion has started or ended. You cannot change the date");
            }
        }
    }
}