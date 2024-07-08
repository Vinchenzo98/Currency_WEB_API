using Currency.API.Models.DTO;

namespace Currency.API.Services.Interfaces
{
    public interface IAccountTypeServices
    {
        Task<AccountTypeDTO> createAccountTypeServices(
                string currencyTag,
                int userId);

        Task<List<AccountTypeDTO>> getAllUserAccountsServices(int userID);

        Task<AccountTypeDTO> getUserAccountServices(int userId, string currencyTag);

        Task<AccountTypeDTO> updateAmountServices(decimal amount, int userId, string currencyTag);
    }
}