using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : Controller
    {
        private readonly IUserLoginServices _userLoginServices;

        public UserLoginController(IUserLoginServices userLoginServices)
        {
            _userLoginServices = userLoginServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(UserLoginRequest loginRequest)
        {
            var user = await _userLoginServices.UserLoginService(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return NotFound("User does not exist");
            }
            return Ok(user);
        }
    }
}
