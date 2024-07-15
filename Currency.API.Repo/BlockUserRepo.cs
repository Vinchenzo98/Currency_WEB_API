using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Currency.API.Repo
{
    public class BlockUserRepo : IBlockUserRepo
    {
        private readonly CurrencyAPIContext _currencyAPIContext;

        public BlockUserRepo(CurrencyAPIContext currencyAPIContext)
        {
            _currencyAPIContext = currencyAPIContext;
        }

        public async Task<BlockedUserLogModelAPI> createBlockedUserRepo(
         BlockedUserLogModelAPI blockedUser
        )
        {
            _currencyAPIContext.Add(blockedUser);
            await _currencyAPIContext.SaveChangesAsync();
            return blockedUser;
        }

        public async Task<BlockedUserLogModelAPI> getBlockedUserRepo(int userId)
        {
            var blockedUser = await _currencyAPIContext.BlockedUserLog.FirstOrDefaultAsync(u => u.UserID == userId);
            if (blockedUser == null)
            {
                return null;
            }
            return blockedUser;
        }
    }
}