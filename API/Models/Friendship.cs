using System;
using static API.Models.Enums.FriendshipEnum;

namespace API.Models
{
    public class Friendship
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid FriendId { get; set; }
        public User Friend { get; set; }
        public FriendshipState State { get; set; }
    }
}
