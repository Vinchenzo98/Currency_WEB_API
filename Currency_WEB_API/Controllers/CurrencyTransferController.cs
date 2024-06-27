using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyTransferController : ControllerBase
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly IGetUserFromTokenService _userFromTokenService;
        private readonly IUserInformationServices _userInformationServices;
        private readonly IUserLoginServices _userLoginServices;

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
                return Unauthorized(currentUserId + "not found now");
            }

            var getPayeeId = await _userInformationServices.GetUserByTagService(transferAmountRequest.userTag);

            //get user then compare currency tags in accounts to see if conversion needs to happen

            decimal convertToNegative = -Math.Abs(transferAmountRequest.amount);

            var getUserAccount = await _accountTypeServices.updateAmountServices(convertToNegative, currentUserId, transferAmountRequest.currencyTag);

            if (getUserAccount == null)
            {
                return BadRequest("Account balance is negative: " + getUserAccount.Amount + " transfer not allowed");
            }

            var getPayeeAccount = await _accountTypeServices.updateAmountServices(transferAmountRequest.amount, getPayeeId.UserID, transferAmountRequest.currencyTag);

            if (getPayeeAccount == null)
            {
                return NotFound("User " + getPayeeId.UserTag + "does not have an account");
            }

            return Ok(getUserAccount);
        }
    }
}