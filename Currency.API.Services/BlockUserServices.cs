using Currency.API.Models.DTO;
using Currency.API.Services.Interfaces;

namespace Currency.API.Services
{
    public class BlockUserServices : IBlockUserServices
    {
        public BlockUserServices()
        { }

        public async Task<BlockUserDTO> createBlockedUserServices(int userId, int adminId)
        {
        }
    }
}