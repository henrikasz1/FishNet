using System;
using System.Threading.Tasks;
using API.Dtos.EventDtos;
using API.Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class OccasionService : IOccasionService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;
        
        public OccasionService(
            DataContext dataContext,
            IUserAccessorService userAccessorService)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
        }

        public async Task AddOccasion(AddOccasionDto occasion)
        {
            var UserId = _userAccessorService.GetCurrentUserId();

            var newOccasion = new Occasion()
            {
                OccasionId = Guid.NewGuid(),
                UserId = Guid.Parse(UserId),
                Title = occasion.Title,
                Description = occasion.Description,
                Location = occasion.Location,
                StartsAt = occasion.StartsAt,
                EndsAt = occasion.EndsAt,
            };

            _dataContext.Occasions.Add(newOccasion);
            
            var result = await _dataContext.SaveChangesAsync() > 0;
            
            if (!result)
            {
                throw new DbUpdateException("Failed to create an occasion");
            }
        }

        public async Task<GetOccasionDto> GetOccasionById(Guid occasionId)
        {
            var occasion = await _dataContext.Occasions
                .Include(y => y.User)
                .FirstOrDefaultAsync(x => x.OccasionId == occasionId);
            // var post = await _dataContext.Posts.Include(x => x.Photos)
            //     .FirstOrDefaultAsync(y => y.PostId == postId);
            
            var newOccasion = new GetOccasionDto
            {
                OccasionId = occasion.OccasionId,
                UserId = occasion.UserId,
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
            
            var userId = _userAccessorService.GetCurrentUserId();

            if (occasion.UserId != Guid.Parse(userId))
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
        //public async Task GetEventsByUserId(GetEventDto )
        //public async Task UpdateEvent(UpdateEventDto )
        //public async Task DeleteEvent(DeleteEventDto )
    }
}