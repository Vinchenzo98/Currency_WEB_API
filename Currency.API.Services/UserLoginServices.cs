using Currency.API.Models.DTO;
using Currency.API.Repo.Interfaces;
using Currency.API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Currency.API.Services
{
    public class UserLoginServices : IUserLoginServices
    {
        private readonly IConfiguration _configuration;
        private readonly IUserLoginRepo _userLoginRepo;

        public UserLoginServices(IUserLoginRepo userLoginRepo, IConfiguration configuration)
        {
            _userLoginRepo = userLoginRepo;
            _configuration = configuration;
        }

        public async Task<UserModelDTO> GetUserByIdService(int userId)
        {
            var authUser = await _userLoginRepo.GetUserByIdRepo(userId);

            return new UserModelDTO
            {
                UserID = authUser.UserID,
                Email = authUser.Email,
                FirstName = authUser.FirstName,
                LastName = authUser.LastName,
                Mobile = authUser.Mobile,
                UserTag = authUser.UserTag,
                Status = authUser.Status
            };
        }

        public async Task<UserModelDTO> UserLoginService(string email, string password)
        {
            var authUser = await _userLoginRepo.AuthLoginRepo(email);

            if (authUser == null)
            {
                return null;
            }

            if (authUser != null && BCrypt.Net.BCrypt.Verify(password, authUser.PasswordHash))
            {
                var token = GenerateJwtToken(new UserModelDTO
                {
                    UserID = authUser.UserID,
                    Email = authUser.Email,
                    FirstName = authUser.FirstName,
                    LastName = authUser.LastName,
                    Mobile = authUser.Mobile,
                    UserTag = authUser.UserTag,
                    Status = authUser.Status
                });

                var user = new UserModelDTO
                {
                    UserID = authUser.UserID,
                    Email = authUser.Email,
                    FirstName = authUser.FirstName,
                    LastName = authUser.LastName,
                    Mobile = authUser.Mobile,
                    UserTag = authUser.UserTag,
                    Status = authUser.Status,
                    Token = token
                };
                return user;
            }
            return null;
        }

        private string GenerateJwtToken(UserModelDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["UserJwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
             new Claim(JwtRegisteredClaimNames.Sub, user.Email),
             new Claim("userId", user.UserID.ToString()),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["UserJwt:Issuer"],
                audience: _configuration["UserJwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}