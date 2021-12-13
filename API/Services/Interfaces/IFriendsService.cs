using API.Dtos.FriendsDtos;
using API.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IFriendsService
    {
        Task<FriendRequestResponse> SendFriendRequest(Guid userId);
        Task<FriendRequestResponse> ApproveFriendRequest(Guid friendId);
        Task<FriendRequestResponse> DeclineFriendRequest(Guid friendId);
        Task<IList<GetFriendsDto>> GetAllFriendsList();
        Task<FriendRequestResponse> Unfriend(Guid friendId);
    }
}
