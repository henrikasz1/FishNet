using API.Dtos.UserDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IList<GetUserDto>> GetUserByName(string filter);
    }
}
