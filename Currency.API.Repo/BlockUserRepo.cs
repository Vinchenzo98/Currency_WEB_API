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

        public async Task<List<BlockedUserLogModelAPI>> getUnBlockedUserRepo(int userId)
        {
            var blockedUser = _currencyAPIContext.BlockedUserLog
                                .Where(u => u.UserID == userId)
                                .ToList();

            if (blockedUser == null)
            {
                return null;
            }
            return blockedUser;
        }

        public async Task<BlockedUserLogModelAPI> removeBlockedUserRepo(
              BlockedUserLogModelAPI blockedUser
     )
        {
            _currencyAPIContext.Remove(blockedUser);
            await _currencyAPIContext.SaveChangesAsync();
            return blockedUser;
        }

        public async Task<BlockedUserLogModelAPI> updateBlockedUserRepo(BlockedUserLogModelAPI blockedUser)
        {
            var trackedEntity = _currencyAPIContext.ChangeTracker.Entries<BlockedUserLogModelAPI>()
                .FirstOrDefault(e => e.Entity.AccountID == blockedUser.AccountID &&
                                     e.Entity.AdminID == blockedUser.AdminID &&
                                     e.Entity.UserID == blockedUser.UserID &&
                                     e.Entity.BlockDate == blockedUser.BlockDate);

            if (trackedEntity != null)
            {
                _currencyAPIContext.Entry(trackedEntity.Entity).State = EntityState.Detached;
            }

            _currencyAPIContext.Update(blockedUser);
            await _currencyAPIContext.SaveChangesAsync();
            return blockedUser;
        }

    }
}