using API.Dtos.SearchDtos;
using API.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<GetUserDtoV2> GetUserById(Guid id);

        Task<IList<GetSearchResultsDto>> GetUserByName(string filter);

        Task<string> GetUserName(Guid uid);
    }
}
