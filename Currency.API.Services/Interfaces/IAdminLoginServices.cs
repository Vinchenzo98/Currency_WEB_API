

using Currency.API.Models.DTO;

namespace Currency.API.Services.Interfaces
{
    public interface IAdminLoginServices
    {
        Task<AdminModelDTO> GetAdminByIdService(int adminId);
        Task<AdminModelDTO> AdminLoginService(string email, string password);
    }
}
