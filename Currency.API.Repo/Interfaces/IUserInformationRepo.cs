using Currency.API.Models;


namespace Currency.API.Repo.Interfaces
{
    public interface IUserInformationRepo
    {
        Task<UsersModelAPI> getUserByTagRepo(string userTag);
    }
}
