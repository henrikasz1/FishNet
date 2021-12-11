using API.Dtos.SearchDtos;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("{filter}")]
        public async Task<ActionResult<IList<GetSearchResultsDto>>> Search(string filter)
        {
            var result = await _searchService.Search(filter);

            return Ok(result);
        }
    }
}
