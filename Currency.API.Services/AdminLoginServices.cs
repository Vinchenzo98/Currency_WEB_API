
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
    public class AdminLoginServices : IAdminLoginServices
    {
        private readonly IAdminLoginRepo _adminLoginRepo;
        private readonly IConfiguration _configuration;

        public AdminLoginServices(IAdminLoginRepo adminLoginRepo, IConfiguration configuration)
        {
            _adminLoginRepo = adminLoginRepo;
            _configuration = configuration;
        }


        public async Task<AdminModelDTO> GetAdminByIdService(int adminId)
        {
            var authAdmin = await _adminLoginRepo.GetAdminByIdRepo(adminId);

            return new AdminModelDTO
            {
                AdminID = authAdmin.AdminID,
                Email = authAdmin.Email,
                FirstName = authAdmin.FirstName,
                LastName = authAdmin.LastName,
                Mobile = authAdmin.Mobile
            };
        }

        public async Task<AdminModelDTO> AdminLoginService(string email, string password)
        {

            var authAdmin = await _adminLoginRepo.AuthLoginRepo(email);

            if (authAdmin == null)
            {
                return null;
            }

            if (authAdmin != null && BCrypt.Net.BCrypt.Verify(password, authAdmin.Password))
            {
                var token = GenerateJwtToken(new AdminModelDTO
                {
                    AdminID = authAdmin.AdminID,
                    Email = authAdmin.Email,
                    FirstName = authAdmin.FirstName,
                    LastName = authAdmin.LastName,
                    Mobile = authAdmin.Mobile
                });


                var admin = new AdminModelDTO
                {
                    AdminID = authAdmin.AdminID,
                    Email = authAdmin.Email,
                    FirstName = authAdmin.FirstName,
                    LastName = authAdmin.LastName,
                    Mobile = authAdmin.Mobile,
                    Token = token
                };
                return admin;
            }
            return null;
        }

        private string GenerateJwtToken(AdminModelDTO admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AdminJwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
             new Claim(JwtRegisteredClaimNames.Sub, admin.Email),
             new Claim("adminId", admin.AdminID.ToString()),
 };

            var token = new JwtSecurityToken(
                issuer: _configuration["AdminJwt:Issuer"],
                audience: _configuration["AdminJwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
