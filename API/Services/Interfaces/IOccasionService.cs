using API.Dtos.EventDtos;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IOccasionService
    {
        Task AddOccasion(AddOccasionDto occasion);
        Task<GetOccasionDto> GetOccasionById(Guid eventId);
        Task DeleteOccasionById(Guid eventId);
    }
}