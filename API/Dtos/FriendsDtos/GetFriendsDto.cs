using System;

namespace API.Dtos.FriendsDtos
{
    public class GetFriendsDto
    {
        public Guid FriendId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MainImageUrl { get; set; }
    }
}
