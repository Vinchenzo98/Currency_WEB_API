using Currency.API.Models;

namespace Currency.API.Repo.Interfaces
{
    public interface IBlockUserRepo
    {
        Task<BlockedUserLogModelAPI> createBlockedUserRepo(
         BlockedUserLogModelAPI blockedUser
        );
    }
}