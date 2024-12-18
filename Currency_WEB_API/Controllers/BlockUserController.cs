﻿using Currency.API.Services.Interfaces;
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

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("block-user-account")]
        public async Task<IActionResult> BlockUserAccount(BlockUserRequest blockUserRequest)
        {
            var adminId = _getAdminTokenFromService.GetAdminIdFromToken();

            var admin = await _adminLoginServices.GetAdminByIdService(adminId);

            if (admin == null)
            {
                return Unauthorized(adminId + "not found");
            }

            var getUserId = await _userInformationServices.GetUserByTagService(blockUserRequest.userTag);

            var getUserAccount = await _accountTypeServices.getUserAccountServices(getUserId.UserID, blockUserRequest.currencyTag);

            var isUnblocked = await _blockUserServices.getUnBlockedUserServices(getUserId.UserID);

            if (isUnblocked != null && isUnblocked.UnblockDate != null)
            {
                await _blockUserServices.removeBlockedUserServices(
                    getUserAccount.AccountID,
                    admin.AdminID,
                    getUserAccount.UserID
                 );
            }

            var blockedUser = await _blockUserServices.createBlockedUserServices(
                    getUserAccount.AccountID,
                    admin.AdminID,
                    getUserAccount.UserID
                );

            await _userInformationServices.updateUserStatus(blockUserRequest.userTag);
            return Ok(blockedUser);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("unblock-user-account")]
        public async Task<IActionResult> UnBlockUserAccount(UnBlockUserRequest unBlockUserRequest)
        {
            var adminId = _getAdminTokenFromService.GetAdminIdFromToken();

            var admin = await _adminLoginServices.GetAdminByIdService(adminId);

            if (admin == null)
            {
                return Unauthorized(adminId + "not found");
            }

            var getUserId = await _userInformationServices.GetUserByTagService(unBlockUserRequest.userTag);
            var getBlockedUser = await _blockUserServices.getBlockedUserServices(getUserId.UserID);
            var unBlockedUser = await _blockUserServices.updateBlockedUserServices(
                    getBlockedUser.AccountID,
                    admin.AdminID,
                    getBlockedUser.UserID,
                    getBlockedUser.BlockDate
                );

            await _userInformationServices.updateUserStatus(unBlockUserRequest.userTag);
            return Ok(unBlockedUser);
        }
    }
}