using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Currency.API.Repo
{
    public class UserRegisterRepo :IUserRegisterRepo
    {
        private readonly CurrencyAPIContext _currencyAPIContext;
        public UserRegisterRepo(CurrencyAPIContext currencyAPIContext) {
        
            _currencyAPIContext = currencyAPIContext;
        }


        public async Task<UsersModelAPI> AuthRegisterRepo(string email)
        {
            var user = await _currencyAPIContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
       
        }
        public async Task<UsersModelAPI> RegisterUserRepo(UsersModelAPI userModel)
        {
            _currencyAPIContext.Users.Add(userModel);
             await _currencyAPIContext.SaveChangesAsync();
            return userModel;
        }
    }
}
