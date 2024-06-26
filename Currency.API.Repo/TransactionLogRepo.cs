using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Currency.API.Repo
{
    public class TransactionLogRepo : ITransactionLogRepo
    {
        private readonly CurrencyAPIContext _currencyAPIContext;

        public TransactionLogRepo(CurrencyAPIContext currencyAPIContext)
        {
            _currencyAPIContext = currencyAPIContext;
        }

        public async Task<TransactionLogModelAPI> createTransactionRepo(
           TransactionLogModelAPI transactionLog
            )
        {
            _currencyAPIContext.Add(transactionLog);
            await _currencyAPIContext.SaveChangesAsync();
            return transactionLog;
        }

        public async Task<TransactionLogModelAPI> getUserTransaction(
                int userID,
                int currencyID,
                decimal amount
            )
        {
            var getTransaction = await _currencyAPIContext.TransactionLog.FirstOrDefaultAsync(
                    log =>
                    log.UserID == userID &&
                    log.CurrencyID == currencyID &&
                    log.Amount == amount
                );
            return getTransaction;
        }
    }
}