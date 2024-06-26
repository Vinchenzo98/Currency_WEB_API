using Currency.API.Models;

namespace Currency.API.Repo.Interfaces
{
   public interface IAdminLoginRepo
    {
        Task<AdminsModelAPI> GetAdminByIdRepo(int adminId);
        Task<AdminsModelAPI> AuthLoginRepo(string email);
    }
}
