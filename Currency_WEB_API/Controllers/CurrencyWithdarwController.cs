using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyWithdarwController : ControllerBase
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly IGetUserFromTokenService _userFromTokenServices;
        private readonly IUserLoginServices _userLoginServices;
        public CurrencyWithdarwController(
            IAccountTypeServices accountTypeServices,
            IGetUserFromTokenService userFromTokenService,
            IUserLoginServices userLoginServices
            )
        {
            _accountTypeServices = accountTypeServices;
            _userFromTokenServices = userFromTokenService;
            _userLoginServices = userLoginServices;
        }

        [HttpPost("withdraw")]
        [Authorize(Policy = "UserPolicy")]

        public async Task<IActionResult> WithdrawAmount(WithdrawRequest withdrawRequest)
        {
            var userId = _userFromTokenServices.GetUserIdFromToken();

            var user = await _userLoginServices.GetUserByIdService(userId);

            if (user == null)
            {
                return Unauthorized(userId + "not found");
            }

            decimal convertToNegative = -Math.Abs(withdrawRequest.Amount);

            var withdrawAmount = await _accountTypeServices.updateAmountServices(convertToNegative, userId);

            //take from bank account in api

            return Ok(withdrawAmount);
        }
    }
}
