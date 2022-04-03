using API.Dtos.SearchDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IList<GetSearchResultsDto>> GetUserByName(string filter);

        Task<string> GetUserName(Guid uid);
    }
}
