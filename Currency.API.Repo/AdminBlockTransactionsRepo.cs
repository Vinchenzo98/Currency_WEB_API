using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Currency.API.Repo
{
    public class AdminBlockTransactionsRepo : IAdminBlockTransactionsRepo
    {
        private readonly CurrencyAPIContext _currencyAPIContext;

        public AdminBlockTransactionsRepo(CurrencyAPIContext currencyAPIContext)
        {
            _currencyAPIContext = currencyAPIContext;
        }

        public async Task<BlockedTransactionsModelAPI> createBlockedTransactionRepo(
            BlockedTransactionsModelAPI blockedTransaction
         )
        {
            _currencyAPIContext.Add(blockedTransaction);
            await _currencyAPIContext.SaveChangesAsync();
            return blockedTransaction;
        }

        public async Task<BlockedTransactionsModelAPI> GetBlockedTransactionRepo(
                int userID
            )
        {
            var blockedTransaction = await _currencyAPIContext.BlockedTransactions.FirstOrDefaultAsync(
                bt => bt.UserID == userID
            );
            if (blockedTransaction == null)
            {
                return null;
            }
            return blockedTransaction;
        }

        public async Task<BlockedTransactionsModelAPI> updateBlockedStatusRepo(BlockedTransactionsModelAPI blockedTransaction)
        {
            _currencyAPIContext.Update(blockedTransaction);
            _currencyAPIContext.SaveChanges();
            return blockedTransaction;
        }
    }
}