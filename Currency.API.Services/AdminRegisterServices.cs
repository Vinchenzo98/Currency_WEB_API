using Currency.API.Models.DTO;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Currency.API.Services.Interfaces;

namespace Currency.API.Services
{
    public class AdminRegisterServices : IAdminRegisterServices
    {

        private readonly IAdminRegisterRepo _adminRegisterRepo;
        public AdminRegisterServices(IAdminRegisterRepo adminRegisterRepo)
        {
            _adminRegisterRepo = adminRegisterRepo;
        }


        public async Task<AdminModelDTO> RegisterAdminServices
        (
        string FirstName,
        string LastName,
        string Mobile,
        string Email,
        string Password
            )
        {
            var authAdmin = await _adminRegisterRepo.AuthRegisterRepo(Email);

            if (authAdmin!= null)
            {
                return null;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
            string adminEmail = "Approved";
            var registerAdmin = new AdminsModelAPI
            {
                FirstName = FirstName,
                LastName = LastName,
                Mobile = Mobile,
                Email = Email,
                Password = hashedPassword,
                IsValidEmail = adminEmail
            };

            await _adminRegisterRepo.RegisterAdminRepo(registerAdmin);

            var admin = new AdminModelDTO
            {
                FirstName = FirstName,
                LastName = LastName,
                Mobile = Mobile,
                Email = Email
            };
            return admin;
        }
    }

}
