using API.Dtos.SearchDtos;
using API.Services.Interfaces;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOccasionService _occasionService;
        private readonly IUserService _userService;

        public SearchService(IOccasionService occasionService, IUserService userService)
        {
            _occasionService = occasionService;
            _userService = userService;
        }

        public async Task<IList<GetSearchResultsDto>> Search(string filter)
        {
            var users = await _userService.GetUserByName(filter);
            var occasions = await _occasionService.GetOccasionByName(filter);
            //var groups = await _groupService.GetGroupByName(filter);
            var searchResultList = new List<GetSearchResultsDto>();

            foreach (var user in users)
            {
                searchResultList.Add(user);

            }

            foreach (var occasion in occasions)
            {
                searchResultList.Add(occasion);
            }

            return searchResultList;
        }
    }
}
