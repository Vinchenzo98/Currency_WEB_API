using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        private readonly IAdminLoginServices _adminLoginServices;

        public AdminLoginController(IAdminLoginServices adminLoginServices)
        {
            _adminLoginServices = adminLoginServices;
        }

        [HttpPost("login-portal")]
        public async Task<IActionResult> AdminLogin(AdminLoginRequest loginRequest)
        {
            var admin = await _adminLoginServices.AdminLoginService(loginRequest.Email, loginRequest.AdminPassword);

            if (admin == null)
            {
                return Ok("Admin does not exist");
            }
            return Ok(admin);
        }
    }
}