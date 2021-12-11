using API.Dtos.EventDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public interface IOccasionService
    {
        Task AddOccasion(IFormFile file, AddOccasionDto occasion);
        Task<GetOccasionDto> GetOccasionById(Guid occasionId);
        Task<IList<GetOccasionDto>> GetAllOccasions();
        Task<IList<GetOccasionDto>> GetOccasionsByHostId(Guid hostId);
        //Task<IList<GetOccasionUsersDto>> GetOccasionUsersByOccasionId(Guid occasionId);
        Task DeleteOccasionById(Guid eventId);
        Task EditOccasionByOccasionId(Guid occasionId, EditOccasionDto newOccasion);
        Task JoinOccasion(Guid occasionId);
    }
}