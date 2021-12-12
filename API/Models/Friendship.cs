using System;
using static API.Models.Enums.FriendshipEnum;

namespace API.Models
{
    public class Friendship
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public FriendshipState State { get; set; }
    }
}
