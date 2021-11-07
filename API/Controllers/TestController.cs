using API.Services;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessor;

        public TestController(
            DataContext dataContext, IUserAccessorService userAccessor)
        {
            _dataContext = dataContext;
            _userAccessor = userAccessor;
        }

        [HttpGet]
        [Route("Id")]
        public IActionResult CheckUserData()
        {
            var userId = _userAccessor.GetCurrentUserId();

            return Ok(userId);
        }
    }
}
