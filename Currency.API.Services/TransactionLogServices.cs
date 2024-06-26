using Currency.API.Models;
using Currency.API.Models.DTO;
using Currency.API.Repo.Interfaces;
using Currency.API.Services.Interfaces;

namespace Currency.API.Services
{
    public class TransactionLogServices : ITransactionLogServices
    {
        private readonly ITransactionLogRepo _transactionLogRepo;

        public TransactionLogServices(ITransactionLogRepo transactionLogRepo)
        {
            _transactionLogRepo = transactionLogRepo;
        }

        public async Task<TransactionLogDTO> createTransactionServices(
                int accountID,
                int currencyID,
                int userID,
                DateTime timeStamp,
                decimal amount
            )
        {
            var createTransaction = new TransactionLogModelAPI
            {
                AccountID = accountID,
                CurrencyID = currencyID,
                UserID = userID,
                TimeSent = timeStamp,
                Amount = amount
            };

            var transactionAdd = await _transactionLogRepo.createTransactionRepo(createTransaction);

            var transactionDTO = new TransactionLogDTO
            {
                AccountID = transactionAdd.AccountID,
                CurrencyID = transactionAdd.CurrencyID,
                UserID = transactionAdd.UserID,
                TimeSent = transactionAdd.TimeSent,
                Amount = transactionAdd.Amount
            };

            return transactionDTO;
        }

        public async Task<TransactionLogDTO> getUserTransactionServices(
                int userID,
                int currencyID,
                decimal amount
            )
        {
            var getTransaction = await _transactionLogRepo.getUserTransaction(
                userID,
                currencyID,
                amount
            );

            var transactionDTO = new TransactionLogDTO
            {
                AccountID = getTransaction.AccountID,
                TimeSent = getTransaction.TimeSent,
                CurrencyID = getTransaction.CurrencyID,
                UserID = getTransaction.UserID,
                Amount = getTransaction.Amount
            };

            return transactionDTO;
        }
    }
}