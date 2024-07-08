using Currency.API.Models;
using Currency.API.Models.DTO;
using Currency.API.Repo.Interfaces;
using Currency.API.Services.Interfaces;

namespace Currency.API.Services
{
    public class TransactionLogServices : ITransactionLogServices
    {
        private readonly ICurrencyExchangeRepo _currencyExchangeRepo;
        private readonly ITransactionLogRepo _transactionLogRepo;

        public TransactionLogServices(
            ITransactionLogRepo transactionLogRepo,
            ICurrencyExchangeRepo currencyExchangeRepo
            )
        {
            _transactionLogRepo = transactionLogRepo;
            _currencyExchangeRepo = currencyExchangeRepo;
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

        public List<TransactionLogDTO> getAllTransactionsServices(int userID, string currencyTag, int accountID)
        {
            var getAllTransaction = _transactionLogRepo.getAllTransactionRepo(userID, accountID);

            var allTransactionsDTO = new List<TransactionLogDTO>();

            foreach (var transaction in getAllTransaction)
            {
                var transactionDTO = new TransactionLogDTO
                {
                    TimeSent = transaction.TimeSent,
                    Amount = transaction.Amount,
                    CurrencyTag = currencyTag
                };

                allTransactionsDTO.Add(transactionDTO);
            }

            return allTransactionsDTO;
        }

        public List<TransactionLogDTO> getNegativeAmountServices(int userID, string currencyTag, int accountID)
        {
            var getNegative = _transactionLogRepo.getNegativeTransactionRepo(userID, accountID);

            var negativeAmountDTO = new List<TransactionLogDTO>();

            foreach (var transaction in getNegative)
            {
                var amountDTO = new TransactionLogDTO
                {
                    TimeSent = transaction.TimeSent,
                    Amount = transaction.Amount,
                    CurrencyTag = currencyTag
                };

                negativeAmountDTO.Add(amountDTO);
            }

            return negativeAmountDTO;
        }

        public List<TransactionLogDTO> getPositiveAmountServices(int userID, string currencyTag, int accountID)
        {
            var getAllTransaction = _transactionLogRepo.getPositiveTransactionRepo(userID, accountID);

            var positiveAmountDTO = new List<TransactionLogDTO>();

            foreach (var transaction in getAllTransaction)
            {
                var amountDTO = new TransactionLogDTO
                {
                    TimeSent = transaction.TimeSent,
                    Amount = transaction.Amount,
                    CurrencyTag = currencyTag
                };
                positiveAmountDTO.Add(amountDTO);
            }

            return positiveAmountDTO;
        }

        public async Task<TransactionLogDTO> getUserTransactionByIDServices(
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