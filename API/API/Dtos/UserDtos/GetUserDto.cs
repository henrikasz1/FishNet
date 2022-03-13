using API.Models;
using System;

namespace API.Dtos.UserDtos
{
    public class GetUserDto
    {
        public Guid UserId { get; set; }
        public string MainUserPhotoUrl { get; set; }
        public string Name { get; set; }
    }
}
