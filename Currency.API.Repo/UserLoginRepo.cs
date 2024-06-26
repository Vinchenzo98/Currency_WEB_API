using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Currency.API.Repo
{
    public class UserLoginRepo : IUserLoginRepo
    {
        private readonly CurrencyAPIContext _currencyAPIContext;

        public UserLoginRepo(CurrencyAPIContext currencyAPIContext)
        {
            _currencyAPIContext = currencyAPIContext;
        }


        public async Task<UsersModelAPI> GetUserByIdRepo(int userId)
        {
            var user = await _currencyAPIContext.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<UsersModelAPI> AuthLoginRepo(string email)
        {
            var user = await _currencyAPIContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
