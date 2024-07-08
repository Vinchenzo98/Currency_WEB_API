using Currency.API.Models.DTO;

namespace Currency.API.Services.Interfaces
{
    public interface ITransactionLogServices
    {
        Task<TransactionLogDTO> createTransactionServices(
                int accountID,
                int currencyID,
                int userID,
                DateTime timeStamp,
                decimal amount
            );

        List<TransactionLogDTO> getAllTransactionsServices(
            int userID,
            string currencyTag,
            int accountID
            );

        List<TransactionLogDTO> getNegativeAmountServices(
            int userID,
            string currencyTag,
            int accountID);

        List<TransactionLogDTO> getPositiveAmountServices(
                    int userID,
            string currencyTag,
            int accountID);

        Task<TransactionLogDTO> getUserTransactionByIDServices(
                int userID,
                int currencyID,
                decimal amount
            );
    }
}