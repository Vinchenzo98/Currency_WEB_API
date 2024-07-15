using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : Controller
    {
        private readonly IBlockUserServices _blockUserServices;
        private readonly IUserInformationServices _userInformationServices;
        private readonly IUserLoginServices _userLoginServices;

        public UserLoginController(
            IUserLoginServices userLoginServices,
            IBlockUserServices blockUserServices,
            IUserInformationServices userInformationServices
            )
        {
            _userLoginServices = userLoginServices;
            _blockUserServices = blockUserServices;
            _userInformationServices = userInformationServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(UserLoginRequest loginRequest)
        {
            var getExistingUser = await _userInformationServices.getUserByEmailServices(loginRequest.Email);

            var isUserBanned = await _blockUserServices.getBlockedUserServices(getExistingUser.UserID);

            if (isUserBanned != null)
            {
                return Ok("User is banned");
            }

            var user = await _userLoginServices.UserLoginService(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Ok("User does not exist");
            }
            return Ok(user);
        }
    }
}