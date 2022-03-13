using API.Dtos.EventDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API.Dtos.SearchDtos;

namespace API.Services
{
    public interface IOccasionService
    {
        Task AddOccasion(IFormFile file, AddOccasionDto occasion);
        Task<GetOccasionDto> GetOccasionById(Guid occasionId);
        Task<List<GetSearchResultsDto>> GetOccasionByName(string filter);
        Task<IList<GetOccasionDto>> GetAllOccasions();
        Task<IList<GetOccasionDto>> GetOccasionsByHostId(Guid hostId);
        Task DeleteOccasionById(Guid eventId);
        Task EditOccasionByOccasionId(Guid occasionId, EditOccasionDto newOccasion);
        Task JoinOccasion(Guid occasionId);
        Task LeaveOccasion(Guid occasionId);
    }
}