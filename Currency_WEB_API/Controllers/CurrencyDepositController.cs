using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyDepositController : ControllerBase
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly IGetUserFromTokenService _userFromTokenServices;
        private readonly IUserLoginServices _userLoginServices;

        public CurrencyDepositController(
            IAccountTypeServices accountTypeServices,
            IGetUserFromTokenService userFromTokenService,
            IUserLoginServices userLoginServices
            )
        {
            _accountTypeServices = accountTypeServices;
            _userFromTokenServices = userFromTokenService;
            _userLoginServices = userLoginServices;
        }

        [HttpPost("deposit")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> DepositAmount(DepositRequest depositRequest)
        {
            var userId = _userFromTokenServices.GetUserIdFromToken();

            var user = await _userLoginServices.GetUserByIdService(userId);

            if (user == null)
            {
                return Unauthorized(userId + "not found");
            }

            var depositAmount = await _accountTypeServices.updateAmountServices(depositRequest.Amount, userId, depositRequest.currencyTag);

            //take from bank account in api

            return Ok(depositAmount);
        }
    }
}