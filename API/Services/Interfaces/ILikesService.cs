using System;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ILikesService
    {
        Task LikePost(Guid postId, Guid loverId);
        Task UnlikePost(Guid postId, Guid loverId);
        Task LikeUserPhoto(string photoId, Guid loverId);
        Task UnlikePhoto(string photoId, Guid loverId);
    }
}
