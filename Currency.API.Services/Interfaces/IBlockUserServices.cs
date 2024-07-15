using Currency.API.Models.DTO;

namespace Currency.API.Services.Interfaces
{
    public interface IBlockUserServices
    {
        Task<BlockUserDTO> createBlockedUserServices(int accountId, int adminId, int userId);

        Task<BlockUserDTO> getBlockedUserServices(int userId);
    }
}