using API.Dtos.UserDtos;
using API.Services.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IList<GetUserDto>> GetUserByName(string filter)
        {
            var users = await _dataContext.Users
                .Include(x => x.Photos)
                .Where(x => x.FirstName.ToLower().Contains(filter.ToLower()) || x.LastName.ToLower().Contains(filter.ToLower()))
                .ToListAsync();

            var userDto = new List<GetUserDto>();

            foreach (var user in users)
            {
                var userMainPhoto = user.Photos.Any() ? user.Photos.FirstOrDefault(x => x.IsMain == true).Url : string.Empty;

                userDto.Add(new GetUserDto
                {
                    UserId = user.UserId,
                    MainUserPhotoUrl = userMainPhoto,
                    Name = user.FirstName + " " + user.LastName
                });
            }

            return userDto == null
                ? throw new Exception("Could not find anything")
                : userDto;
        }
    }
}
