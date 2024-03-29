﻿using API.Dtos.SearchDtos;
using API.Dtos.UserDtos;
using API.Services.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static API.Models.Enums.SearchResultEnum;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<GetUserDtoV2> GetUserById(Guid id)
        {
            var user = await _dataContext.Users
                .Include(x => x.Photos)
                .Where(x => x.UserId == id)
                .SingleOrDefaultAsync();

            var userMainPhoto = user.Photos.Where(x => x.IsMain == true).Any() ? user.Photos.FirstOrDefault(x => x.IsMain == true).Url : string.Empty;

            var userDto = new GetUserDtoV2
            {
                UserId = user.UserId,
                MainUserPhotoUrl = userMainPhoto,
                Name = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsProfilePrivate = user.IsProfilePrivate
            };

            return userDto == null
               ? throw new Exception("Could not find anything")
               : userDto;
        }

        public async Task<IList<GetSearchResultsDto>> GetUserByName(string filter)
        {
            filter = filter.Replace('%', ' ');

            var users = await _dataContext.Users
                .Include(x => x.Photos)
                .Where(x => (x.FirstName.ToLower() + " " + x.LastName.ToLower()).Contains(filter.ToLower()))
                .ToListAsync();

            var userDto = new List<GetSearchResultsDto>();

            foreach (var user in users)
            {
                var userMainPhoto = user.Photos.Where(x => x.IsMain == true).Any() ? user.Photos.FirstOrDefault(x => x.IsMain == true).Url : string.Empty;

                userDto.Add(new GetSearchResultsDto
                {
                    EntityId = user.UserId,
                    EntityMainPhotoUrl = userMainPhoto,
                    EntityName = user.FirstName + " " + user.LastName,
                    EntityType = SearchResultType.User
                });
            }

            return userDto == null
                ? throw new Exception("Could not find anything")
                : userDto;
        }

        public async Task<string> GetUserName(Guid uid){

            var user = await _dataContext.Users.SingleOrDefaultAsync(
                x => x.UserId == uid);

            var profileName = user.FirstName + " " + user.LastName;

            return profileName;
        }
    }
}
