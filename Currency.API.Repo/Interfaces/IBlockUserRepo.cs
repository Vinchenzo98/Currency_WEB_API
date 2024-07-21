using Currency.API.Models;

namespace Currency.API.Repo.Interfaces
{
    public interface IBlockUserRepo
    {
        Task<BlockedUserLogModelAPI> createBlockedUserRepo(
         BlockedUserLogModelAPI blockedUser
        );

        Task<List<BlockedUserLogModelAPI>> getUnBlockedUserRepo(int userId);

        Task<BlockedUserLogModelAPI> removeBlockedUserRepo(
              BlockedUserLogModelAPI blockedUser
     );

        Task<BlockedUserLogModelAPI> updateBlockedUserRepo(BlockedUserLogModelAPI blockedUser);
    }
}