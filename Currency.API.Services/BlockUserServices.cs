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
            var getBlockedUser = await _blockUserRepo.getUnBlockedUserRepo(userId);

            if (getBlockedUser == null)
            {
                return null;
            }

            var blockedUserList = new List<BlockUserDTO>();

            foreach (var user in getBlockedUser)
            {
                var blockedUserDTO = new BlockUserDTO
                {
                    UserID = user.UserID,
                    AccountID = user.AccountID,
                    BlockDate = user.BlockDate,
                    UnblockDate = user.UnblockDate
                };
                blockedUserList.Add(blockedUserDTO);
            }

            var findBlockedAccount = blockedUserList
                                    .FirstOrDefault(x =>
                                        x.BlockDate.HasValue &&
                                        x.UnblockDate == null
                                        );

            return findBlockedAccount;
        }

        public async Task<BlockUserDTO> getUnBlockedUserServices(int userId)
        {
            var getUnBlockedUser = await _blockUserRepo.getUnBlockedUserRepo(userId);

            if (getUnBlockedUser == null)
            {
                return null;
            }

            var unBlockedUserList = new List<BlockUserDTO>();

            foreach (var user in getUnBlockedUser)
            {
                var unBlockedUserDTO = new BlockUserDTO
                {
                    UserID = user.UserID,
                    AccountID = user.AccountID,
                    BlockDate = user.BlockDate,
                    UnblockDate = user.UnblockDate
                };
                unBlockedUserList.Add(unBlockedUserDTO);
            }

            var findBlockedAccount = unBlockedUserList
                                    .FirstOrDefault(x => x.UnblockDate != null);

            return findBlockedAccount;
        }

        public async Task<BlockUserDTO> removeBlockedUserServices(int accountId, int adminId, int userId)
        {
            var blockUser = new BlockedUserLogModelAPI
            {
                AccountID = accountId,
                AdminID = adminId,
                UserID = userId
            };

            var addBlockedUser = await _blockUserRepo.removeBlockedUserRepo(blockUser);

            var blockUserDTO = new BlockUserDTO
            {
                AccountID = addBlockedUser.AccountID,
                AdminID = addBlockedUser.AdminID,
                UserID = addBlockedUser.UserID
            };

            return blockUserDTO;
        }

        public async Task<BlockUserDTO> updateBlockedUserServices(int accountId, int adminId, int userId, DateTime? blockDate)
        {
            DateTime unBlockDate = DateTime.UtcNow;
            var unBlockedUser = new BlockedUserLogModelAPI
            {
                AccountID = accountId,
                AdminID = adminId,
                UserID = userId,
                BlockDate = blockDate,
                UnblockDate = unBlockDate
            };

            var removeBlockedUser = await _blockUserRepo.updateBlockedUserRepo(unBlockedUser);

            var blockUserDTO = new BlockUserDTO
            {
                UnblockDate = removeBlockedUser.UnblockDate
            };

            return blockUserDTO;
        }
    }
}