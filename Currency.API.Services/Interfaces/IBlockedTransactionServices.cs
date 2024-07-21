using Currency.API.Models.DTO;

namespace Currency.API.Services.Interfaces
{
    public interface IBlockedTransactionServices
    {
        Task<BlockedTransactionDTO> createBlockedTransactionService(
            int userId,
            int currencyId,
            int accountId,
            decimal amount,
            DateTime timeSent,
            string reason,
            int adminID
        );

        Task<BlockedTransactionDTO> GetBlockedTransactionByUserID(int userID);

        Task<BlockedTransactionDTO> updateBlockedTransactionService(
                    int userId,
            int currencyId,
            int accountId,
            decimal amount,
            DateTime timeSent,
            string reason,
            int adminID
        );
    }
}