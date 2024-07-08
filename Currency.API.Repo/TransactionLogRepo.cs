using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

        public List<TransactionLogModelAPI> getAllTransactionRepo(int userID, int accountID)
        {
            return _currencyAPIContext.TransactionLog.Where(
                t => t.UserID == userID &&
                t.AccountID == accountID)
            .ToList();
        }

        public List<TransactionLogModelAPI> getNegativeTransactionRepo(int userID, int accountID)
        {
            return _currencyAPIContext.TransactionLog.Where(
                tl => 
                tl.UserID == userID &&
                tl.AccountID == accountID &&
                tl.Amount < 0
                ).ToList();
        }

        public List<TransactionLogModelAPI> getPositiveTransactionRepo(int userID, int accountID)
        {
            return _currencyAPIContext.TransactionLog.Where(
                tl =>
                tl.UserID == userID &&
                tl.AccountID == accountID &&
                tl.Amount >= 0
                ).ToList();
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