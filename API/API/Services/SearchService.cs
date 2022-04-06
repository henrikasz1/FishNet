using API.Dtos.SearchDtos;
using API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOccasionService _occasionService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        
        public SearchService(IOccasionService occasionService, IUserService userService, IGroupService groupService)
        {
            _occasionService = occasionService;
            _userService = userService;
            _groupService = groupService;
        }

        public async Task<IList<GetSearchResultsDto>> Search(string filter)
        {
            var users = await _userService.GetUserByName(filter);
            var occasions = await _occasionService.GetOccasionByName(filter);
            var groups = await _groupService.GetGroupByName(filter);
            var searchResultList = new List<GetSearchResultsDto>();

            foreach (var user in users)
            {
                if (user.EntityMainPhotoUrl == "")
                {
                    user.EntityMainPhotoUrl = "https://res.cloudinary.com/dvqm06uor/image/upload/v1649245447/default-user-image_uhfifb.jpg";
                }

                searchResultList.Add(user);
            }

            foreach (var occasion in occasions)
            {
                if (occasion.EntityMainPhotoUrl == "")
                {
                    occasion.EntityMainPhotoUrl = "https://res.cloudinary.com/dvqm06uor/image/upload/v1649247389/default-event-image_ke6ll6.jpg";
                }

                searchResultList.Add(occasion);
            }

            foreach (var group in groups)
            {
                if (group.EntityMainPhotoUrl == null)
                {
                    group.EntityMainPhotoUrl = "https://res.cloudinary.com/dvqm06uor/image/upload/v1649245630/default-group-image_bkvyni.jpg";
                }

                searchResultList.Add(group);
            }

            return searchResultList;
        }
    }
}
