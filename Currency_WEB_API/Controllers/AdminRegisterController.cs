using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminRegisterController : Controller
    {
        private readonly IAdminRegisterServices _adminRegisterServices;

        public AdminRegisterController(IAdminRegisterServices adminRegisterServices)
        {
            _adminRegisterServices = adminRegisterServices;
        }

        [HttpPost("register-portal")]
        public async Task<IActionResult> AdminRegister(AdminRegisterRequest adminRegister)
        {

            var registerAdmin = await _adminRegisterServices.RegisterAdminServices(
                    adminRegister.FirstName,
                    adminRegister.LastName,  
                    adminRegister.Mobile,
                    adminRegister.Email,
                    adminRegister.Password
                );
            if (registerAdmin == null)
            {
                return NotFound("Admin allready exists");
            }

            return Ok(registerAdmin);
        }
    }
}
