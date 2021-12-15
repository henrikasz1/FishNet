using System;

namespace API.Dtos.LikesDto
{
    public class GetLikesDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MainPhotoUrl { get; set; }
    }
}