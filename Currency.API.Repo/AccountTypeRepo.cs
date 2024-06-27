using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Currency.API.Repo
{
    public class AccountTypeRepo : IAccountTypeRepo
    {
        private readonly CurrencyAPIContext _currencyAPIContext;

        public AccountTypeRepo(CurrencyAPIContext currencyAPIContext)
        {
            _currencyAPIContext = currencyAPIContext;
        }

        public async Task<AccountTypeModelAPI> createAccountTypeRepo(AccountTypeModelAPI accountModel)
        {
            await _currencyAPIContext.AddAsync(accountModel);
            _currencyAPIContext.SaveChangesAsync();
            return accountModel;
        }

        public async Task<AccountTypeModelAPI> getUserAccountRepo(int userId, string currencyTag)
        {
            var account = await _currencyAPIContext.AccountType.FirstOrDefaultAsync(
                 log =>
                    log.UserID == userId &&
                    log.AccountType == currencyTag
                    );
            if (account == null)
            {
                return null;
            }
            return account;
        }

        public async Task<AccountTypeModelAPI> updateAmountRepo(AccountTypeModelAPI accountModel)
        {
            var updateAccount = await _currencyAPIContext.AccountType.FirstOrDefaultAsync(a => a.AccountID == accountModel.AccountID);

            if (updateAccount != null)
            {
                updateAccount.Amount = accountModel.Amount;
                await _currencyAPIContext.SaveChangesAsync();
                return updateAccount;
            }
            else
            {
                return null;
            }
        }
    }
}