using Currency.API.Models.DTO;

namespace Currency.API.Services.Interfaces
{
    public interface IBlockUserServices
    {
        Task<BlockUserDTO> createBlockedUserServices(int accountId, int adminId, int userId);

        Task<BlockUserDTO> getBlockedUserServices(int userId);

        Task<BlockUserDTO> getUnBlockedUserServices(int userId);

        Task<BlockUserDTO> removeBlockedUserServices(int accountId, int adminId, int userId);

        Task<BlockUserDTO> updateBlockedUserServices(int accountId, int adminId, int userId, DateTime? blokckDate);
    }
}