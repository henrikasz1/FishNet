using API.Dtos.FriendsDtos;
using API.Dtos.Responses;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static API.Models.Enums.FriendshipEnum;

namespace API.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;

        public FriendsService(DataContext dataContext, IUserAccessorService userAccessorService)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
        }

        public async Task<FriendRequestResponse> SendFriendRequest(Guid userId)
        {
            var response = new FriendRequestResponse();
            var user = await _dataContext.Users
                .Include(x => x.Friends)
                .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(_userAccessorService.GetCurrentUserId()));

            var friend = await _dataContext.Users
                .Include(x => x.Friends)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (friend == null)
            {
                response.Status = "User not found";
                return response;
            }

            var userRecord = new Friendship()
            {
                UserId = user.UserId,
                FriendId = friend.UserId,
                State = FriendshipState.Pending
            };

            var friendRecord = new Friendship()
            {
                UserId = friend.UserId,
                FriendId = user.UserId,
                State = FriendshipState.Pending
            };
            
            if (user.Friends.Any(x => x.FriendId == userRecord.FriendId && x.UserId == userRecord.UserId) 
                || friend.Friends.Any(x => x.FriendId == friendRecord.FriendId && x.UserId == friendRecord.UserId))
            {
                response.Status = "Friends request has already been sent";
                if (user.Friends.Any(x => x.State == FriendshipState.Friends))
                {
                    response.Status = "You are already friends with this user";
                }
                return response;
            }
            else if (userRecord.UserId == userRecord.FriendId)
            {
                response.Status = "You cannot send fiend request to yourself";
                return response;
            }
            else
            {
                user.Friends.Add(userRecord);
                friend.Friends.Add(friendRecord);
                response.Status = "Friend request sent successfully";
            }

            var result = await _dataContext.SaveChangesAsync() > 0;
            if (!result) response.Status = "Failed to send friend request";

            return response;
        }

        public async Task<FriendRequestResponse> ApproveFriendRequest(Guid friendId)
        {
            var userFriendship = await _dataContext.Friends
                .Where(x => x.UserId == Guid.Parse(_userAccessorService.GetCurrentUserId()) && x.FriendId == friendId)
                .FirstOrDefaultAsync();
            var friendFriendship = await _dataContext.Friends
                .Where(x => x.FriendId == Guid.Parse(_userAccessorService.GetCurrentUserId()) && x.UserId == friendId)
                .FirstOrDefaultAsync();

            userFriendship.State = FriendshipState.Friends;
            friendFriendship.State = FriendshipState.Friends;

            var result = await _dataContext.SaveChangesAsync() > 0;

            return !result
                ? new FriendRequestResponse { Status = "Failed to approve friend request" }
                : new FriendRequestResponse { Status = "Friend request approved successfully" };
        }

        public async Task<FriendRequestResponse> DeclineFriendRequest(Guid friendId)
        {
            var userFriendship = await _dataContext.Friends
                .Where(x => x.UserId == Guid.Parse(_userAccessorService.GetCurrentUserId()) && x.FriendId == friendId)
                .FirstOrDefaultAsync();
            var friendFriendship = await _dataContext.Friends
                .Where(x => x.FriendId == Guid.Parse(_userAccessorService.GetCurrentUserId()) && x.UserId == friendId)
                .FirstOrDefaultAsync();

            _dataContext.Friends.Remove(userFriendship);
            _dataContext.Friends.Remove(friendFriendship);

            var result = await _dataContext.SaveChangesAsync() > 0;

            return !result
                ? new FriendRequestResponse { Status = "Failed to decline friend request" }
                : new FriendRequestResponse { Status = "Friend request declined successfully" };
        }

        public async Task<IList<GetFriendsDto>> GetAllFriendsList()
        {
            var user = await _dataContext.Users
                .Include(x => x.Friends)
                .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(_userAccessorService.GetCurrentUserId()));

            var friends = new List<User>();

            foreach (var friend in user.Friends)
            {
                friends.Add(await _dataContext.Users
                    .Include(x => x.Photos)
                    .Where(x => x.UserId == friend.FriendId)
                    .FirstOrDefaultAsync());
            }

            var response = new List<GetFriendsDto>();

            foreach (var friend in friends)
            {
                var userMainPhoto = friend.Photos.Any() ? friend.Photos.FirstOrDefault(x => x.IsMain == true).Url : string.Empty;

                response.Add(new GetFriendsDto
                {
                    FriendId = friend.UserId,
                    FirstName = friend.FirstName,
                    LastName = friend.LastName,
                    MainImageUrl = userMainPhoto
                });
            }

            return response;
        }

        public async Task<FriendRequestResponse> Unfriend(Guid friendId)
        {
            var userFriendship = await _dataContext.Friends
               .Where(x => x.UserId == Guid.Parse(_userAccessorService.GetCurrentUserId()) && x.FriendId == friendId)
               .FirstOrDefaultAsync();
            var friendFriendship = await _dataContext.Friends
                .Where(x => x.FriendId == Guid.Parse(_userAccessorService.GetCurrentUserId()) && x.UserId == friendId)
                .FirstOrDefaultAsync();

            _dataContext.Friends.Remove(userFriendship);
            _dataContext.Friends.Remove(friendFriendship);

            var result = await _dataContext.SaveChangesAsync() > 0;

            return !result
                ? new FriendRequestResponse { Status = "Failed to unfriend user" }
                : new FriendRequestResponse { Status = "User has been successfully unfriended" };
        }
    }
}
