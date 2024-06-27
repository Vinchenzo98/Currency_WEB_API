using Currency.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.API.Repo.Interfaces
{
    public interface IAccountTypeRepo
    {
        Task<AccountTypeModelAPI> createAccountTypeRepo(AccountTypeModelAPI accountModel);

        Task<AccountTypeModelAPI> getUserAccountRepo(int userId, string currencyTag);

        Task<AccountTypeModelAPI> updateAmountRepo(AccountTypeModelAPI accountModel);
    }
}