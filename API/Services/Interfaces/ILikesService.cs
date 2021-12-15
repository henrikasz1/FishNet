using System;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ILikesService
    {
        Task LikePost(Guid postId);
        Task UnlikePost(Guid postId, Guid loverId);
        Task LikeUserPhoto(string photoId, Guid loverId);
        Task UnlikePhoto(string photoId, Guid loverId);
        Task LikeComment(Guid commentId);
        Task UnlikeComment(Guid commentId);
    }
}
