using API.Dtos;
using API.Dtos.Responses;
using API.Services;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Data;
using API.Dtos.UserDtos;
using API.Services.Interfaces;
using API.Dtos.SearchDtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManagementService _authManagementService;
        private readonly UserManager<User> _userManager;
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly string CurrentUserId;

        public UserController(
            IAuthManagementService authManagementService,
            UserManager<User> userManager,
            IUserAccessorService userAccessorService,
            DataContext dataContext,
            IUserService userService)
        {
            _authManagementService = authManagementService;
            _userManager = userManager;
            _dataContext = dataContext;
            _userService = userService;
            CurrentUserId = userAccessorService.GetCurrentUserId();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            //Check if valid
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                // Check if email is not occupied
                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Email already in use"
                        },
                        Success = false
                    });
                }

                var newUser = new User()
                {
                    UserId = Guid.NewGuid(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.Email,
                    IsProfilePrivate = user.IsProfilePrivate
                };

                var IsCreated = await _userManager.CreateAsync(newUser, user.Password);

                if (IsCreated.Succeeded)
                {
                    var jwtToken = _authManagementService.GenerateJwtToken(newUser);

                    return Ok(new RegistrationResponse()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = IsCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }
            }

            //If not valid, then log error
            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Invalid Payload"
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest(new LoginResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid login request"
                        },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (!isCorrect)
                {
                    return BadRequest(new LoginResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid login request"
                        },
                        Success = false
                    });
                }

                var jwtToken = _authManagementService.GenerateJwtToken(existingUser);

                return Ok(new LoginResponse()
                {
                    Success = true,
                    Token = jwtToken
                });
            }

            return BadRequest(new LoginResponse()
            {
                Errors = new List<string>()
                {
                    "Invalid payload"
                },
                Success = false
            });
        }

        [HttpPut("Update/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUserAccount(Guid id, [FromBody] UpdateUserAccountDto userUpdate)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(
                x => x.UserId == Guid.Parse(CurrentUserId));

            if (id != user.UserId)
            {
                return Unauthorized();
            }

            if (await _dataContext.Users.AnyAsync(x => x.Email == userUpdate.Email && x.UserId != user.UserId)
                && userUpdate.Email != user.Email)
            {
                return BadRequest("Email is already in use");
            }

            if (userUpdate.FirstName != null && userUpdate.FirstName != user.FirstName)
            {
                user.FirstName = userUpdate.FirstName;
            }

            if (userUpdate.LastName != null && userUpdate.LastName != user.LastName)
            {
                user.LastName = userUpdate.LastName;
            }

            if (userUpdate.Email != null && userUpdate.Email != user.Email)
            {
                user.Email = userUpdate.Email;
            }

            if (user.IsProfilePrivate != userUpdate.IsProfilePrivate) user.IsProfilePrivate = userUpdate.IsProfilePrivate;

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("getbyname/{filter}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IList<GetSearchResultsDto>>> GetUserByName(string filter)
        {
            var result = await _userService.GetUserByName(filter);

            return Ok(result);
        }

        [HttpGet("getbyid/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<GetUserDtoV2>> GetUserById(Guid id)
        {
            var result = await _userService.GetUserById(id);

            return Ok(result);
        }

        [HttpGet("getuserid")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public string GetUserId()
        {
            return CurrentUserId;
        }

        [HttpGet("getname/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> GetUserName(Guid id)
        {
            var result = await _userService.GetUserName(id);

            return Ok(result);
        }

        [HttpGet("checkpassword/{password}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<bool>> CheckPassword(string password)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(
                x => x.UserId == Guid.Parse(CurrentUserId));

            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            return await _userManager.CheckPasswordAsync(existingUser, password);
        }

        [HttpPut("changepassword/{password}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ChangePassword([FromHeader] string Authorization, string password)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(
                x => x.UserId == Guid.Parse(CurrentUserId));

            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _userManager.ResetPasswordAsync(existingUser, token, password);

            return Ok();
        }
    }
}
