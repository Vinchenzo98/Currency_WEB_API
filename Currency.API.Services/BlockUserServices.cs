using Currency.API.Models;
using Currency.API.Models.DTO;
using Currency.API.Repo.Interfaces;
using Currency.API.Services.Interfaces;

namespace Currency.API.Services
{
    public class BlockUserServices : IBlockUserServices
    {
        private readonly IBlockUserRepo _blockUserRepo;

        public BlockUserServices(IBlockUserRepo blockUserRepo)
        {
            _blockUserRepo = blockUserRepo;
        }

        public async Task<BlockUserDTO> createBlockedUserServices(int accountId, int adminId, int userId)
        {
            DateTime blockDate = DateTime.UtcNow;

            var blockUser = new BlockedUserLogModelAPI
            {
                AccountID = accountId,
                AdminID = adminId,
                UserID = userId,
                BlockDate = blockDate,
            };

            var addBlockedUser = await _blockUserRepo.createBlockedUserRepo(blockUser);

            var blockUserDTO = new BlockUserDTO
            {
                AccountID = addBlockedUser.AccountID,
                AdminID = addBlockedUser.AdminID,
                UserID = addBlockedUser.UserID,
                BlockDate = addBlockedUser.BlockDate
            };

            return blockUserDTO;
        }

        public async Task<BlockUserDTO> getBlockedUserServices(int userId)
        {
            var getBlockedUser = await _blockUserRepo.getBlockedUserRepo(userId);

            if (getBlockedUser == null)
            {
                return null;
            }

            var blockedUserDTO = new BlockUserDTO
            {
                UserID = getBlockedUser.UserID,
                BlockDate = getBlockedUser.BlockDate
            };

            return blockedUserDTO;
        }
    }
}