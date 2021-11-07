using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountUpdateController : ControllerBase
    {
        public AccountUpdateController()
        {

        }

        [Route("Update")]
        public IActionResult UpdateUserAccount()
        {
            return Ok();
        }
    }
}
