using API.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IOccasionPostService
    {
        Task AddOccasionPost(List<IFormFile> files, Guid occasionId, AddPostDto post);
        Task DeleteOccasionPost(Guid postId);
        Task<IList<GetPostDto>> GetAllOccasionPosts(Guid occasionId);
    }
}
