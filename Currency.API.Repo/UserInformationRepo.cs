using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Currency.API.Repo
{
    public class UserInformationRepo : IUserInformationRepo
    {
        private readonly CurrencyAPIContext _currencyAPIContext;

        public UserInformationRepo(CurrencyAPIContext currencyAPIContext)
        {
            _currencyAPIContext = currencyAPIContext;
        }

        public async Task<UsersModelAPI> getUserByTagRepo(string userTag)
        {
            var user = await _currencyAPIContext.Users.FirstOrDefaultAsync(u => u.UserTag == userTag);
            return user;
        }

        public async Task<UsersModelAPI> getUserByIdRepo(int userId)
        {
            var user = await _currencyAPIContext.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            return user;
        }

    }
}
