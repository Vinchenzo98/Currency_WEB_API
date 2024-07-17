using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly IAdminLoginServices _adminLoginServices;
        private readonly IGetAdminTokenFromService _getAdminTokenFromService;
        private readonly IUserInformationServices _userInformationServices;

        public AdminDashboardController(
            IUserInformationServices userInformationServices,
            IGetAdminTokenFromService adminTokenFromService,
            IAdminLoginServices adminLoginServices,
            IAccountTypeServices accountTypeServices
            )
        {
            _userInformationServices = userInformationServices;
            _getAdminTokenFromService = adminTokenFromService;
            _adminLoginServices = adminLoginServices;
            _accountTypeServices = accountTypeServices;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("get-accounts")]
        public async Task<IActionResult> GetAllUserAccounts(UserAccountsRequest userAccounts)
        {
            var adminId = _getAdminTokenFromService.GetAdminIdFromToken();

            var admin = await _adminLoginServices.GetAdminByIdService(adminId);

            if (admin == null)
            {
                return Unauthorized(adminId + "not found");
            }

            var getUserId = await _userInformationServices.GetUserByTagService(userAccounts.userTag);

            var getAllUserAccounts = await _accountTypeServices.getAllUserAccountsServices(getUserId.UserID);

            return Ok(getAllUserAccounts);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("get-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var adminId = _getAdminTokenFromService.GetAdminIdFromToken();

            var admin = await _adminLoginServices.GetAdminByIdService(adminId);

            if (admin == null)
            {
                return Unauthorized(adminId + "not found");
            }

            var getAllUsers = await _userInformationServices.getAllUsersService();
            return Ok(getAllUsers);
        }
    }
}