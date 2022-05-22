using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.UserDtos
{
    public class GetUserDtoV2
    {
        public Guid UserId { get; set; }
        public string MainUserPhotoUrl { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool IsProfilePrivate { get; set; }
    }
}
