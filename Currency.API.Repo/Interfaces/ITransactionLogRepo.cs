using Currency.API.Models;

namespace Currency.API.Repo.Interfaces
{
    public interface ITransactionLogRepo
    {
        Task<TransactionLogModelAPI> createTransactionRepo(TransactionLogModelAPI transactionLog);

        List<TransactionLogModelAPI> getAllTransactionRepo(int userID, int accountID);

        List<TransactionLogModelAPI> getNegativeTransactionRepo(int userID, int accountID);

        List<TransactionLogModelAPI> getPositiveTransactionRepo(int userID, int accountID);

        Task<TransactionLogModelAPI> getUserTransaction(
                                int userID,
                int currencyID,
                decimal amount
            );
    }
}