using API.Dtos.CommentsDtos;
using API.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.LikesDto;

namespace API.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentResponse> AddComment(Guid postId, CommentDto body);
        Task<IList<GetCommentDto>> GetAllPostComments(Guid postId);
        Task<CommentResponse> DeleteComment(Guid commentId);
        Task<CommentResponse> UpdateComment(Guid commentId, CommentDto newComment);
        Task<IList<GetLikesDto>> GetCommentLikesById(Guid commentId);
    }
}
