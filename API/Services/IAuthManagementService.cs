using API.Models;

namespace API.Services
{
    public interface IAuthManagementService
    {
        string GenerateJwtToken(User user);
    }
}
