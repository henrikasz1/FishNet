using System;
using System.Threading.Tasks;
using API.Dtos.EventDtos;
using API.Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class EventService : IEventService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;
        
        public EventService(
            DataContext dataContext,
            IUserAccessorService userAccessorService)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
        }

        public async Task AddEvent(AddEventDto occasion)
        {
            var UserId = _userAccessorService.GetCurrentUserId();

            var newEvent = new Event()
            {
                EventId = Guid.NewGuid(),
                UserId = Guid.Parse(UserId),
                Title = occasion.Title,
                Description = occasion.Description,
                Location = occasion.Location,
                StartsAt = occasion.StartsAt,
                EndsAt = occasion.EndsAt,
            };

            _dataContext.Event.Add(newEvent);
            
            var result = await _dataContext.SaveChangesAsync() > 0;
            
            if (!result)
            {
                throw new DbUpdateException("Failed to create an event");
            }
        }

        public async Task<GetEventDto> GetEventById(Guid eventId)
        {
            var occasion = await _dataContext.Event
                // .Include(y => y.User)
                .FirstOrDefaultAsync(x => x.EventId == eventId);
            // var post = await _dataContext.Posts.Include(x => x.Photos)
            //     .FirstOrDefaultAsync(y => y.PostId == postId);
            
            var newEvent = new GetEventDto
            {
                EventId = occasion.EventId,
                UserId = occasion.UserId,
                Title = occasion.Title,
                Description = occasion.Description,
                Location = occasion.Location,
                StartsAt = occasion.StartsAt,
                EndsAt = occasion.EndsAt,
            };
        
            return newEvent;
        }

        public async Task DeleteEventById(Guid eventId)
        {
            var Event = await _dataContext.Event.FirstOrDefaultAsync(x => x.EventId == eventId);
            
            var userId = _userAccessorService.GetCurrentUserId();

            if (Event.UserId != Guid.Parse(userId))
            {
                throw new UnauthorizedAccessException("Only the host can delete the event");
            }

            _dataContext.Event.Remove(Event);
        }
        //public async Task GetEventsByUserId(GetEventDto )
        //public async Task UpdateEvent(UpdateEventDto )
        //public async Task DeleteEvent(DeleteEventDto )
    }
}