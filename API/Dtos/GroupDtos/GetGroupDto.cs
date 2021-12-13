using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.Dtos.GroupDtos
{
    public class GetGroupDto
    {
        public Guid GroupId { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public GroupPhoto Photo { get; set; }
        
        public List<Guid> MembersIds { get; set; }
        
        public int MembersCount { get; set; } = 0;
    }
}