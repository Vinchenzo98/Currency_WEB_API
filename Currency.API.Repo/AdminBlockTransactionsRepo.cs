using Currency.API.DAL;
using Currency.API.Models;
using Microsoft.EntityFrameworkCore;
using Currency.API.Repo.Interfaces;

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

        public async Task<BlockedTransactionsModelAPI> GetBlockedTransactionRepo(int blockedTransactionID)
        {
            var blockedTransaction = await _currencyAPIContext.BlockedTransactions.FirstOrDefaultAsync(
                bt => bt.BlockedTransactionsID == blockedTransactionID
            );
            if (blockedTransaction == null)
            {
                return null;
            }
            return blockedTransaction;
        }

        public async Task<BlockedTransactionsModelAPI> updateBlockedStatusRepo(BlockedTransactionsModelAPI blockedTransaction)
        {
            var getBlockedTransaction = await _currencyAPIContext.BlockedTransactions.FirstOrDefaultAsync(bt => bt.BlockedTransactionsID == blockedTransaction.BlockedTransactionsID);

            if (getBlockedTransaction == null)
            {
                blockedTransaction.BlockedTime = DateTime.UtcNow;
                _currencyAPIContext.BlockedTransactions.Add(blockedTransaction);
            }
            else
            {
                getBlockedTransaction.UnBlockedTime = DateTime.UtcNow;
                _currencyAPIContext.BlockedTransactions.Update(blockedTransaction);
            }

            _currencyAPIContext.SaveChanges();
            return blockedTransaction;
        }
    }
}