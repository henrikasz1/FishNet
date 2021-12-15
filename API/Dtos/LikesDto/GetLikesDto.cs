using System;

namespace API.Dtos.LikesDto
{
    public class GetLikesDto
    {
        public Guid UserId;
        public string FirstName;
        public string LastName;
        public string MainPhotoUrl;
    }
}