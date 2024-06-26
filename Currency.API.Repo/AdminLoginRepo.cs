using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Currency.API.Repo
{
    public class AdminLoginRepo : IAdminLoginRepo
    {
        private readonly CurrencyAPIContext _currencyAPIContext;

        public AdminLoginRepo(CurrencyAPIContext currencyAPIContext)
        {
            _currencyAPIContext = currencyAPIContext;
        }


        public async Task<AdminsModelAPI> GetAdminByIdRepo(int adminId)
        {
            var admin = await _currencyAPIContext.Admins.FirstOrDefaultAsync(u => u.AdminID == adminId);
            if (admin == null)
            {
                return null;
            }
            return admin;
        }
        public async Task<AdminsModelAPI> AuthLoginRepo(string email)
        {
            var admin = await _currencyAPIContext.Admins.FirstOrDefaultAsync(u => u.Email == email);
            return admin;
        }
    }
}
