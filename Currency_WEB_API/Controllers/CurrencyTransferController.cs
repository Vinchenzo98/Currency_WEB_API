using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Currency_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyTransferController : ControllerBase
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly IGetUserFromTokenService _userFromTokenService;
        private readonly IUserLoginServices _userLoginServices;
        private readonly IUserInformationServices _userInformationServices;

        public CurrencyTransferController(
          IGetUserFromTokenService userFromTokenService,
          IUserLoginServices userLoginServices,
          IAccountTypeServices accountTypeServices,
          IUserInformationServices userInformationServices
          )
        {
            _userFromTokenService = userFromTokenService;
            _userLoginServices = userLoginServices;
            _accountTypeServices = accountTypeServices;
            _userInformationServices = userInformationServices;
        }

        [HttpPost("transfer")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> TransferCurrency(TransferAmountRequest transferAmountRequest)
        {
            var currentUserId = _userFromTokenService.GetUserIdFromToken();

            var currentUser = await _userLoginServices.GetUserByIdService(currentUserId);

            if (currentUser == null)
            {
                return Unauthorized(currentUserId + "not found");
            }


            var getPayeeId = await _userInformationServices.GetUserByTagService(transferAmountRequest.userTag);

            decimal convertToNegative = -Math.Abs(transferAmountRequest.amount);

            var getUserAccount = await _accountTypeServices.updateAmountServices(convertToNegative, currentUserId);

            if (getUserAccount == null)
            {
                return BadRequest("Account balance is: " + getUserAccount.Amount + " transfer not allowed");
            }

            var getPayeeAccount = await _accountTypeServices.updateAmountServices(transferAmountRequest.amount, getPayeeId.UserID);

            if (getPayeeAccount == null)
            {
                return NotFound("User " + getPayeeId.UserTag + "does not have an account");

            }

            return Ok(getUserAccount);

        }
    }
}
