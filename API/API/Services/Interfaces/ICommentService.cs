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
        
        Task<CommentResponse> DeleteComment(Guid commentId);
        
        Task<IList<GetLikesDto>> GetCommentLikesById(Guid commentId);
    }
}
