using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTransactionController : Controller
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly ITransactionLogServices _transactionLogServices;
        private readonly IGetUserFromTokenService _userFromTokenServices;
        private readonly IUserLoginServices _userLoginServices;

        public UserTransactionController(
            ITransactionLogServices transactionLogServices,
            IGetUserFromTokenService userFromTokenServices,
            IUserLoginServices userLoginServices,
            IAccountTypeServices accountTypeServices
            )
        {
            _transactionLogServices = transactionLogServices;
            _userFromTokenServices = userFromTokenServices;
            _userLoginServices = userLoginServices;
            _accountTypeServices = accountTypeServices;
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("getAll")]
        public async Task<IActionResult> GetAllUserTransactions([FromBody]UserTransactionsRequest transactionsRequest)
        {
            var userId = _userFromTokenServices.GetUserIdFromToken();

            var user = await _userLoginServices.GetUserByIdService(userId);

            if (user == null)
            {
                return Unauthorized(userId + "not found");
            }

            if (string.IsNullOrEmpty(transactionsRequest.currencyTag))
            {
                return BadRequest("Currency Tag is required");
            }

            var getAccount = await _accountTypeServices.getUserAccountServices(userId, transactionsRequest.currencyTag);

            var getAllTransaction = _transactionLogServices.getAllTransactionsServices(
                    user.UserID,
                    transactionsRequest.currencyTag,
                    getAccount.AccountID
                );
            
            return Ok(getAllTransaction);
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("getNegative")]
        public async Task<IActionResult> GetNegativeTransaction(UserTransactionsRequest transactionsRequest)
        {
            var userId = _userFromTokenServices.GetUserIdFromToken();

            var user = await _userLoginServices.GetUserByIdService(userId);

            if (user == null)
            {
                return Unauthorized(userId + "not found");
            }

            var getAccount = await _accountTypeServices.getUserAccountServices(userId, transactionsRequest.currencyTag);

            var getAllTransaction = _transactionLogServices.getNegativeAmountServices(
                    user.UserID,
                    transactionsRequest.currencyTag,
                    getAccount.AccountID
                );

            return Ok(getAllTransaction);
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("getPositive")]
        public async Task<IActionResult> GetPositiveTransaction(UserTransactionsRequest transactionsRequest)
        {
            var userId = _userFromTokenServices.GetUserIdFromToken();

            var user = await _userLoginServices.GetUserByIdService(userId);

            if (user == null)
            {
                return Unauthorized(userId + "not found");
            }

            var getAccount = await _accountTypeServices.getUserAccountServices(userId, transactionsRequest.currencyTag);

            var getAllTransaction = _transactionLogServices.getPositiveAmountServices(
                    user.UserID,
                    transactionsRequest.currencyTag,
                    getAccount.AccountID
                );

            return Ok(getAllTransaction);
        }
    }
}