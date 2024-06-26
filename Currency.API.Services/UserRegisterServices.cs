using Azure.Core;
using Currency.API.Models;
using Currency.API.Models.DTO;
using Currency.API.Repo.Interfaces;
using Currency.API.Services.Interfaces;

namespace Currency.API.Services
{
    public class UserRegisterServices : IUserRegisterServices
    {
        private readonly IUserRegisterRepo _userRegisterRepo;

        public UserRegisterServices(IUserRegisterRepo userRegisterRepo)
        {
            _userRegisterRepo = userRegisterRepo;
        }


        public async Task<UserModelDTO> RegisterUserServices
        (
        string UserTag,
        string FirstName,
        string LastName,
        string Mobile,
        string Email,
        string Password
            )
        {
            var authUser = await _userRegisterRepo.AuthRegisterRepo(Email);

            if (authUser != null)
            {
                return null;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
            string userStatus = "Approved";

            var registerUser = new UsersModelAPI 
            { 
                UserTag = UserTag,
                FirstName = FirstName,
                LastName = LastName,
                Mobile = Mobile,
                Email = Email,
                PasswordHash = hashedPassword,
                Status = userStatus
            };

            await _userRegisterRepo.RegisterUserRepo(registerUser);

            var user = new UserModelDTO
            {
                UserTag = UserTag,
                FirstName = FirstName,
                LastName = LastName,
                Mobile = Mobile,
                Email = Email
            };
            return user;
        }
    }
}
