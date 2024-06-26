using Currency.API.Models;

namespace Currency.API.Repo.Interfaces
{
    public interface IAdminBlockTransactionsRepo
    {
        Task<BlockedTransactionsModelAPI> createBlockedTransactionRepo(BlockedTransactionsModelAPI blockedTransaction);

        Task<BlockedTransactionsModelAPI> GetBlockedTransactionRepo(int blockedTransactionID);

        Task<BlockedTransactionsModelAPI> updateBlockedStatusRepo(BlockedTransactionsModelAPI blockedTransaction);
    }
}