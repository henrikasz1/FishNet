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
        Task<IList<GetPostDto>> GetPostsByUserId(Guid userId, int batchNumber);
        Task<IList<GetPostDto>> GetAllPosts();
        Task<IList<GetPostDto>> GetRemainingPublicPosts(int batchNumber);
        Task UpdatePostById(Guid postId, EditPostDto newPost);
        Task<IList<GetLikesDto>> GetPostLikesById(Guid postId);

        public Task<IList<GetPostDto>> GetAllFriendPosts(int batchNumber);
        public Task DeletePostById(string id);
    }
}
