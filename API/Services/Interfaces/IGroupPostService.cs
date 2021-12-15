using API.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IGroupPostService
    {
        Task AddGroupPost(List<IFormFile> files, Guid groupId, AddPostDto post);
        Task DeleteGroupPost(Guid postId);
        Task<IList<GetPostDto>> GetAllGroupPosts(Guid groupId);
    }
}
