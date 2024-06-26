using Currency.API.Models;

namespace Currency.API.Repo.Interfaces
{
    public interface ITransactionLogRepo
    {
        Task<TransactionLogModelAPI> createTransactionRepo(TransactionLogModelAPI transactionLog);

        Task<TransactionLogModelAPI> getUserTransaction(
                int userID,
                int currencyID,
                decimal amount
            );
    }
}