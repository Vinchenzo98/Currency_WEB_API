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

        Task<TransactionLogDTO> getUserTransactionServices(
                int userID,
                int currencyID,
                decimal amount
            );
    }
}