using API.Dtos.SearchDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ISearchService
    {
        Task<IList<GetSearchResultsDto>> Search(string filter);
    }
}
