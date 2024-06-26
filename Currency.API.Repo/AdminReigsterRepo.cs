using Currency.API.DAL;
using Currency.API.Models;
using Microsoft.EntityFrameworkCore;
using Currency.API.Repo.Interfaces;

namespace Currency.API.Repo
{
    public class AdminReigsterRepo : IAdminRegisterRepo
    {
        private CurrencyAPIContext _currencyAPIContext;

        public AdminReigsterRepo(CurrencyAPIContext currencyAPIContext) 
        {
            _currencyAPIContext = currencyAPIContext;
        }

        public async Task<AdminsModelAPI> AuthRegisterRepo(string email)
        {
            var admin = await _currencyAPIContext.Admins.FirstOrDefaultAsync(u => u.Email == email);
            return admin;

        }
        public async Task<AdminsModelAPI> RegisterAdminRepo(AdminsModelAPI adminModel)
        {
            _currencyAPIContext.Admins.Add(adminModel);
            await _currencyAPIContext.SaveChangesAsync();
            return adminModel;
        }
    }
}
