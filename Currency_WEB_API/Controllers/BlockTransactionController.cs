using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockTransactionController : Controller
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly IAdminLoginServices _adminLoginServices;
        private readonly IBlockedTransactionServices _blockedTransactionServices;
        private readonly IGetAdminTokenFromService _getAdminTokenFromService;
        private readonly ITransactionLogServices _transactionLogServices;
        private readonly IUserInformationServices _userInformationServices;

        public BlockTransactionController(
            IGetAdminTokenFromService getAdminTokenFromService,
            IAdminLoginServices adminLoginServices,
            IBlockedTransactionServices blockedTransactionServices,
            IUserInformationServices userInformationServices,
            ITransactionLogServices transactionLogServices,
            IAccountTypeServices accountTypeServices
            )
        {
            _getAdminTokenFromService = getAdminTokenFromService;
            _adminLoginServices = adminLoginServices;
            _blockedTransactionServices = blockedTransactionServices;
            _userInformationServices = userInformationServices;
            _transactionLogServices = transactionLogServices;
            _accountTypeServices = accountTypeServices;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("block-transaction")]
        public async Task<IActionResult> BlockTransaction(BlockTransactionRequest blockedTransaction)
        {
            var adminId = _getAdminTokenFromService.GetAdminIdFromToken();

            var admin = await _adminLoginServices.GetAdminByIdService(adminId);

            if (admin == null)
            {
                return Unauthorized(adminId + "not found");
            }

            var getUserId = await _userInformationServices.GetUserByTagService(blockedTransaction.userTag);

            var getUserAccount = await _accountTypeServices.getUserAccountServices(getUserId.UserID, blockedTransaction.currencyTag);

            var getTransactionTime = await _transactionLogServices.getUserTransactionByIDServices(
                getUserAccount.UserID,
                getUserAccount.CurrencyID,
                getUserAccount.Amount
            );

            var createBlockedTransaction = await _blockedTransactionServices.createBlockedTransactionService(
                    getTransactionTime.UserID,
                    getTransactionTime.CurrencyID,
                    getTransactionTime.AccountID,
                    getTransactionTime.Amount,
                    getTransactionTime.TimeSent,
                    blockedTransaction.Reason,
                    admin.AdminID
                );

            return Ok(createBlockedTransaction);
        }
    }
}