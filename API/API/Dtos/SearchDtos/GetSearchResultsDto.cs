using System;
using static API.Models.Enums.SearchResultEnum;

namespace API.Dtos.SearchDtos
{
    public class GetSearchResultsDto
    {
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public string EntityMainPhotoUrl { get; set; }
        public SearchResultType EntityType { get; set; }
    }
}
