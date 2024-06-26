using Currency.API.Models.DTO;

namespace Currency.API.Services.Interfaces
{
    public interface IBlockUserServices
    {
        Task<BlockUserDTO> createBlockedUserServices(int userId, int adminId);
    }
}