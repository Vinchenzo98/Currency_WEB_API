using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : Controller
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly IBlockUserServices _blockUserServices;
        private readonly IUserInformationServices _userInformationServices;
        private readonly IUserLoginServices _userLoginServices;
        private readonly IGetUserFromTokenService _userFromTokenService;

        public UserLoginController(
            IUserLoginServices userLoginServices,
            IBlockUserServices blockUserServices,
            IUserInformationServices userInformationServices,
            IAccountTypeServices accountTypeServices,
            IGetUserFromTokenService userFromTokenService
            )
        {
            _userLoginServices = userLoginServices;
            _blockUserServices = blockUserServices;
            _userInformationServices = userInformationServices;
            _accountTypeServices = accountTypeServices;
            _userFromTokenService = userFromTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(UserLoginRequest loginRequest)
        {
            var getExistingUser = await _userInformationServices.getUserByEmailServices(loginRequest.Email);

            var isUserBanned = await _blockUserServices.getBlockedUserServices(getExistingUser.UserID);

            if (isUserBanned != null && isUserBanned.BlockDate != null)
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


        [Authorize(Policy = "UserPolicy")]
        [HttpGet("get-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var userId = _userFromTokenService.GetUserIdFromToken();

            var currentUser = await _userLoginServices.GetUserByIdService(userId);

            if (currentUser == null)
            {
                return Unauthorized(userId + "not found");
            }

            var getAllUsers = await _userInformationServices.getAllUsersService();

            return Ok(getAllUsers);
        }
    }
}