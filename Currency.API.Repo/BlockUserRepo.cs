using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;

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
    }
}