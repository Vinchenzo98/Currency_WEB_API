using Currency.API.Models;

namespace Currency.API.Repo.Interfaces
{
    public interface IUserInformationRepo
    {
        Task<UsersModelAPI> changeUserStatus(UsersModelAPI usersModel);

        List<UsersModelAPI> getAllUsers();

        Task<UsersModelAPI> getUserByEmailRepo(string email);

        Task<UsersModelAPI> getUserByTagRepo(string userTag);
    }
}