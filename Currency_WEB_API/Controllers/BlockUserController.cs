using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockUserController : Controller
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly IAdminLoginServices _adminLoginServices;
        private readonly IBlockUserServices _blockUserServices;
        private readonly IGetAdminTokenFromService _getAdminTokenFromService;
        private readonly IUserInformationServices _userInformationServices;

        public BlockUserController(
            IUserInformationServices userInformationServices,
            IAdminLoginServices adminLoginServices,
            IGetAdminTokenFromService getAdminTokenFromService,
            IAccountTypeServices accountTypeServices,
            IBlockUserServices blockUserServices
        )
        {
            _userInformationServices = userInformationServices;
            _adminLoginServices = adminLoginServices;
            _getAdminTokenFromService = getAdminTokenFromService;
            _accountTypeServices = accountTypeServices;
            _blockUserServices = blockUserServices;
        }

        [HttpPost("block-user")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> BlockUser(BlockUserRequest blockUserRequest)
        {
            var adminId = _getAdminTokenFromService.GetAdminIdFromToken();

            var admin = await _adminLoginServices.GetAdminByIdService(adminId);

            if (admin == null)
            {
                return Unauthorized(adminId + "not found");
            }

            var getUserId = await _userInformationServices.GetUserByTagService(blockUserRequest.userTag);

            var getUserAccount = await _accountTypeServices.getUserAccountServices(getUserId.UserID, blockUserRequest.currencyTag);

            var blockedUser = await _blockUserServices.createBlockedUserServices(
                    getUserAccount.AccountID,
                    admin.AdminID,
                    getUserAccount.UserID
                );

            return Ok(blockedUser);
        }
    }
}