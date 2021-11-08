using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPostService
    {
        Task AddPost(IFormFile file, AddPostDto post);
        Task<GetPostDto> GetPostById(Guid postId);
        Task<IList<GetPostDto>> GetPostsByUserId(Guid userId);
        Task<IList<GetPostDto>> GetAllPosts();
    }
}
