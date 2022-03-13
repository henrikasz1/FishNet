using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class UpdateUserAccountDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public bool IsProfilePrivate { get; set; }
    }
}
