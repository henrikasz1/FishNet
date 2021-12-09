using API.Dtos.EventDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IOccasionService
    {
        Task AddOccasion(AddOccasionDto occasion);
        Task<GetOccasionDto> GetOccasionById(Guid occasionId);
        Task<IList<GetOccasionDto>> GetAllOccasions();
        Task<IList<GetOccasionDto>> GetOccasionsByHostId(Guid hostId);
        Task DeleteOccasionById(Guid eventId);
        Task EditOccasionByOccasionId(Guid occasionId, EditOccasionDto newOccasion);
    }
}