
using Currency.API.Models;

namespace Currency.API.Repo.Interfaces
{
    public interface IAdminRegisterRepo
    {
        Task<AdminsModelAPI> AuthRegisterRepo(string email);
        Task<AdminsModelAPI> RegisterAdminRepo(AdminsModelAPI adminModel);
    }
}
