using API.Dtos.SearchDtos;
using API.Services.Interfaces;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class SearchService : ISearchService
    {
        private readonly DataContext _dataContext;
        private readonly IOccasionService _occasionService;
        private readonly IUserService _userService;

        public SearchService(DataContext dataContext, IOccasionService occasionService, IUserService userService)
        {
            _dataContext = dataContext;
            _occasionService = occasionService;
            _userService = userService;
        }

        //public async Task<IList<GetSearchResultsDto>> Search(string filter)
        //{
        //    var users = _userService.GetUserByName(filter);
        //    //var occasions = _occasionService.
        //}
    }
}
