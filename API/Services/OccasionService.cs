using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Dtos.EventDtos;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

        public async Task<GetOccasionDto> GetOccasionById(Guid occasionId)
        {
            var occasion = await _dataContext.Occasions
                .Include(y => y.User)
                .FirstOrDefaultAsync(x => x.OccasionId == occasionId);

            var newOccasion = new GetOccasionDto
            {
                OccasionId = occasion.OccasionId,
                HostId = occasion.HostId,
                Title = occasion.Title,
                Description = occasion.Description,
                Location = occasion.Location,
                StartsAt = occasion.StartsAt,
                EndsAt = occasion.EndsAt,
            };
        
            return newOccasion;
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
        
        public async Task<IList<GetOccasionDto>> GetAllOccasions()
        {
            var occasionsList = new List<GetOccasionDto>();

            var occasions = await _dataContext.Occasions
                .Select(x => x)
                .Include(y => y.User)
                .ToListAsync();

            foreach (var occasion in occasions)
            {
                occasionsList.Add(
                    new GetOccasionDto
                    {
                        OccasionId = occasion.OccasionId,
                        HostId = occasion.HostId,
                        Title = occasion.Title,
                        Description = occasion.Description,
                        Location = occasion.Location,
                        StartsAt = occasion.StartsAt,
                        EndsAt = occasion.EndsAt,
                    });
            }

            return occasionsList;
        }

        public async Task<IList<GetOccasionDto>> GetOccasionsByHostId(Guid hostId)
        {
            var occasionsList = new List<GetOccasionDto>();

            var occasions = await _dataContext.Occasions.Where(x => x.HostId == hostId)
                .ToListAsync();

            foreach (var occasion in occasions)
            {
                occasionsList.Add(
                    new GetOccasionDto
                    {
                        OccasionId = occasion.OccasionId,
                        HostId = occasion.HostId,
                        Title = occasion.Title,
                        Description = occasion.Description,
                        Location = occasion.Location,
                        StartsAt = occasion.StartsAt,
                        EndsAt = occasion.EndsAt,
                    });
            }

            return occasionsList;
        }

        public async Task EditOccasionByOccasionId(Guid occasionId, EditOccasionDto newOccasion)
        {
            var occasion = await _dataContext.Occasions.FirstOrDefaultAsync(x => x.OccasionId == occasionId);

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