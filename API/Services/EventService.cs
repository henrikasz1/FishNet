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

        public async Task AddEvent(AddEventDto Event)
        {
            var UserId = _userAccessorService.GetCurrentUserId();

            var newEvent = new Event()
            {
                EventId = Guid.NewGuid(),
                UserId = Guid.Parse(UserId),
                Title = Event.Title,
                Description = Event.Description,
                Location = Event.Location,
                StartsAt = Event.StartsAt,
                EndsAt = Event.EndsAt,
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
            var Event = await _dataContext.Event.FirstOrDefaultAsync(x => x.EventId == eventId);
            
            var newEvent = new GetEventDto
            {
                EventId = Event.EventId,
                UserId = Event.UserId,
                Title = Event.Title,
                Description = Event.Description,
                Location = Event.Location,
                StartsAt = Event.StartsAt,
                EndsAt = Event.EndsAt,
            };
        
            return newEvent;
        }
        //public async Task GetEventsByUserId(GetEventDto )
        //public async Task UpdateEvent(UpdateEventDto )
        //public async Task DeleteEvent(DeleteEventDto )
    }
}