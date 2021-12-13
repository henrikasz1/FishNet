using API.Dtos.CommentsDtos;
using API.Dtos.Responses;
using API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentResponse> AddComment(Guid postId, CommentDto body);
        Task<IList<Comment>> GetAllPostComments(Guid postId);
    }
}
