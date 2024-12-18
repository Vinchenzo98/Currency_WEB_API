﻿using Currency.API.Models;
using Currency.API.Models.DTO;
using Currency.API.Repo.Interfaces;
using Currency.API.Services.Interfaces;

namespace Currency.API.Services
{
    public class BlockedTransactionServices : IBlockedTransactionServices
    {
        private readonly IAdminBlockTransactionsRepo _blockTransactionsRepo;
        private readonly ITransactionLogServices _transactionLogServices;

        public BlockedTransactionServices(
            IAdminBlockTransactionsRepo blockTransactionsRepo,
            ITransactionLogServices transactionLogServices
            )
        {
            _blockTransactionsRepo = blockTransactionsRepo;
            _transactionLogServices = transactionLogServices;
        }

        public async Task<BlockedTransactionDTO> createBlockedTransactionService(
                        int userId,
                        int currencyId,
                        int accountId,
                        decimal amount,
                        DateTime timeSent,
                        string reason,
                        int adminID
                    )
        {
            var blockedTime = DateTime.UtcNow;

            var createTransaction = new BlockedTransactionsModelAPI
            {
                AccountID = accountId,
                CurrencyID = currencyId,
                UserID = userId,
                TimeSent = timeSent,
                Amount = amount,
                Reason = reason,
                BlockedTime = blockedTime,
                AdminID = adminID
            };

            var transactionAdd = await _blockTransactionsRepo.createBlockedTransactionRepo(createTransaction);

            var transactionDTO = new BlockedTransactionDTO
            {
                AccountID = transactionAdd.AccountID,
                CurrencyID = transactionAdd.CurrencyID,
                UserID = transactionAdd.UserID,
                TimeSent = transactionAdd.TimeSent,
                Amount = transactionAdd.Amount
            };

            return transactionDTO;
        }

        public async Task<BlockedTransactionDTO> GetBlockedTransactionByUserID(int userID)
        {
            var blockedUserTransaction = await _blockTransactionsRepo.GetBlockedTransactionRepo(userID);

            if (blockedUserTransaction == null)
            {
                return null;
            }

            var blockedUserTransactionDTO = new BlockedTransactionDTO
            {
                Amount = blockedUserTransaction.Amount,
                TimeSent = blockedUserTransaction.TimeSent,
                BlockedTime = blockedUserTransaction.BlockedTime
            };
            return blockedUserTransactionDTO;
        }

        public async Task<BlockedTransactionDTO> updateBlockedTransactionService(
                                    int userId,
                    int currencyId,
                    int accountId,
                    decimal amount,
                    DateTime timeSent,
                    string reason,
                    int adminID
                )
        {
            var unBlockedTime = DateTime.UtcNow;

            var updateTransaction = new BlockedTransactionsModelAPI
            {
                AccountID = accountId,
                CurrencyID = currencyId,
                UserID = userId,
                TimeSent = timeSent,
                Amount = amount,
                Reason = reason,
                UnBlockedTime = unBlockedTime,
                AdminID = adminID
            };

            var transactionUpdate = await _blockTransactionsRepo.updateBlockedStatusRepo(updateTransaction);

            var transactionDTO = new BlockedTransactionDTO
            {
                AccountID = transactionUpdate.AccountID,
                CurrencyID = transactionUpdate.CurrencyID,
                UserID = transactionUpdate.UserID,
                TimeSent = transactionUpdate.TimeSent,
                Amount = transactionUpdate.Amount,
                Reason = transactionUpdate.Reason,
                UnBlockedTime = transactionUpdate.UnBlockedTime
            };

            return transactionDTO;
        }
    }
}