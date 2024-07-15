using Microsoft.AspNetCore.Mvc;
using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;

namespace Currency_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRegisterController : Controller
    {
        private readonly IUserRegisterServices _userRegisterServices;

        public UserRegisterController(
            IUserRegisterServices userRegisterServices
            )
        {
            _userRegisterServices = userRegisterServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest registerRequest)
        {
            var user = await _userRegisterServices.RegisterUserServices(
                registerRequest.UserTag,
                registerRequest.FirstName,
                registerRequest.LastName,
                registerRequest.Mobile,
                registerRequest.Email,
                registerRequest.Password
            );

            if (user == null)
            {
                return Ok("User Already exists");
            }

            return Ok(user);
        }
    }
}