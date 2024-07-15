using Currency.API.Models;

namespace Currency.API.Repo.Interfaces
{
    public interface IAdminBlockTransactionsRepo
    {
        Task<BlockedTransactionsModelAPI> createBlockedTransactionRepo(BlockedTransactionsModelAPI blockedTransaction);

        Task<BlockedTransactionsModelAPI> GetBlockedTransactionRepo(
               int userID
            );

        Task<BlockedTransactionsModelAPI> updateBlockedStatusRepo(BlockedTransactionsModelAPI blockedTransaction);
    }
}