using API.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.LikesDto;

namespace API.Services
{
    public interface IPostService
    {
        Task AddPost(List<IFormFile> file, AddPostDto post);
        Task<GetPostDto> GetPostById(Guid postId);
        Task<IList<GetPostDto>> GetPostsByUserId(Guid userId);
        Task<IList<GetPostDto>> GetAllPosts();
        Task DeletePostById(string id);
        Task UpdatePostById(Guid postId, EditPostDto newPost);
        Task<IList<GetLikesDto>> GetPostLikesById(Guid postId);
    }
}
