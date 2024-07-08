using Currency.API.Models;
using Currency.API.Models.DTO;
using Currency.API.Repo.Interfaces;
using Currency.API.Services.Interfaces;

namespace Currency.API.Services
{
    public class AccountTypeServices : IAccountTypeServices
    {
        private readonly IAccountTypeRepo _accountTypeRepo;
        private readonly ICurrencyExchangeRepo _currencyExchangeRepo;
        private readonly ITransactionLogServices _transactionLogServices;
        private readonly IUserRegisterRepo _userRegisterRepo;

        public AccountTypeServices(
            IAccountTypeRepo accountTypeRepo,
            IUserRegisterRepo userRegisterRepo,
            ICurrencyExchangeRepo currencyExchangeRepo,
            ITransactionLogServices transactionLogServices
            )
        {
            _accountTypeRepo = accountTypeRepo;
            _userRegisterRepo = userRegisterRepo;
            _currencyExchangeRepo = currencyExchangeRepo;
            _transactionLogServices = transactionLogServices;
        }

        public async Task<AccountTypeDTO> createAccountTypeServices(
                string currencyTag,
                int userId
            )
        {
            try
            {
                var currencyAccountExists = await _accountTypeRepo.getUserAccountRepo(userId, currencyTag);

                if (currencyAccountExists != null)
                {
                    if (currencyAccountExists.AccountType != null && currencyAccountExists.UserID != null)
                    {
                        throw new Exception(
                            $"Currency type: {currencyAccountExists.CurrencyType} for {currencyAccountExists.UserID} already exists.");
                    }
                }

                var getCurrency = await _currencyExchangeRepo.getCurrencyByName(currencyTag);

                var amount = 0;

                var registeredAccount = new AccountTypeModelAPI
                {
                    AccountType = getCurrency.CurrencyTag,
                    UserID = userId,
                    CurrencyID = getCurrency.CurrencyID,
                    Amount = amount
                };

                await _accountTypeRepo.createAccountTypeRepo(registeredAccount);

                var account = new AccountTypeDTO
                {
                    AccountType = registeredAccount.AccountType,
                    UserID = registeredAccount.UserID,
                    Amount = registeredAccount.Amount,
                    CurrencyID = getCurrency.CurrencyID
                };
                return account;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<AccountTypeDTO>> getAllUserAccountsServices(int userID)
        {
            try
            {
                var getAllUserAccounts = await _accountTypeRepo.getAllUserAccountsRepo(userID);

                if (getAllUserAccounts == null)
                {
                    throw new Exception("Accounts do not exist");
                }

                var accountsToListDTO = new List<AccountTypeDTO>();

                foreach (var account in getAllUserAccounts)
                {
                    var accountDTO = new AccountTypeDTO
                    {
                        AccountID = account.AccountID,
                        AccountType = account.AccountType,
                        CurrencyID = account.CurrencyID,
                        UserID = account.UserID,
                        Amount = account.Amount,
                    };

                    accountsToListDTO.Add(accountDTO);
                }
                return accountsToListDTO;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<AccountTypeDTO> getUserAccountServices(int userId, string currencyTag)
        {
            try
            {
                var getAccount = await _accountTypeRepo.getUserAccountRepo(userId, currencyTag);

                if (getAccount == null)
                {
                    throw new Exception("Account does not exist");
                }

                var accountDTO = new AccountTypeDTO
                {
                    AccountID = getAccount.AccountID,
                    AccountType = getAccount.AccountType,
                    CurrencyID = getAccount.CurrencyID,
                    UserID = getAccount.UserID,
                    Amount = getAccount.Amount,
                };

                return accountDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AccountTypeDTO> updateAmountServices(decimal amount, int userId, string currencyTag)
        {
            try
            {
                var account = await _accountTypeRepo.getUserAccountRepo(userId, currencyTag);

                if (account == null)
                {
                    throw new Exception("Account does not exist");
                }

                bool isPositiveNumber = amount > 0;

                if (isPositiveNumber)
                {
                    account.Amount += amount;
                }
                else
                {
                    decimal sum = account.Amount + amount;
                    bool isNegativeBalance = sum < 0;

                    if (isNegativeBalance)
                    {
                        throw new Exception("Account is negative transaction failed");
                    }
                    else
                    {
                        account.Amount = sum;
                    }
                }

                await _accountTypeRepo.updateAmountRepo(account);

                DateTime timeStamp = DateTime.UtcNow;

                await _transactionLogServices.createTransactionServices(
                      account.AccountID,
                      account.CurrencyID,
                      account.UserID,
                      timeStamp,
                      amount
                      );

                var accountDTO = new AccountTypeDTO
                {
                    AccountType = account.AccountType,
                    CurrencyID = account.CurrencyID,
                    UserID = account.UserID,
                    Amount = account.Amount,
                };

                return accountDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}