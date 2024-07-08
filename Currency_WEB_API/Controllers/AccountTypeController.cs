using Microsoft.AspNetCore.Mvc;
using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Currency_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountTypeController : Controller
    {
        private readonly IAccountTypeServices _acccountTypeServices;
        private readonly IGetUserFromTokenService _userFromTokenServices;
        private readonly IUserLoginServices _userLoginServices;

        public AccountTypeController(
            IAccountTypeServices acccountTypeServices,
            IGetUserFromTokenService userFromTokenService,
            IUserLoginServices userLoginServices
            )
        {
            _acccountTypeServices = acccountTypeServices;
            _userFromTokenServices = userFromTokenService;
            _userLoginServices = userLoginServices;
        }

        [HttpPost("create-account")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> createAccount([FromBody] AccountTypeRequest accountRequest)
        {
            var userId = _userFromTokenServices.GetUserIdFromToken();

            var user = await _userLoginServices.GetUserByIdService(userId);

            if (user == null)
            {
                return Unauthorized(userId + "not found");
            }

            var accountType = await _acccountTypeServices.createAccountTypeServices
                (
                    accountRequest.CurrencyTag,
                    user.UserID
                );

            if (accountType == null)
            {
                return BadRequest("Currency account allready exists for user");
            }

            return Ok(accountType);
        }

        [HttpGet("get-accounts")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> getAllUserAccounts()
        {
            var userId = _userFromTokenServices.GetUserIdFromToken();

            var user = await _userLoginServices.GetUserByIdService(userId);

            if (user == null)
            {
                return Unauthorized(userId + "not found");
            }

            var getAllUserAccounts = await _acccountTypeServices.getAllUserAccountsServices(userId);

            if (getAllUserAccounts == null)
            {
                return NoContent();
            }
            return Ok(getAllUserAccounts);
        }
    }
}