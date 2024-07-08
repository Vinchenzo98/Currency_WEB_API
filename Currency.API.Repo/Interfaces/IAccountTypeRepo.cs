using Currency.API.Models;

namespace Currency.API.Repo.Interfaces
{
    public interface IAccountTypeRepo
    {
        Task<AccountTypeModelAPI> createAccountTypeRepo(AccountTypeModelAPI accountModel);

        Task<List<AccountTypeModelAPI>> getAllUserAccountsRepo(int userId);

        Task<AccountTypeModelAPI> getUserAccountRepo(int userId, string currencyTag);

        Task<AccountTypeModelAPI> updateAmountRepo(AccountTypeModelAPI accountModel);
    }
}