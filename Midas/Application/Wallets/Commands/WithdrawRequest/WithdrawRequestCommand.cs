using Application.Interfaces;
using Domain.Wallets;
using System;
using System.Linq;

namespace Application.Wallets.Commands.WithdrawRequest
{
    public class WithdrawRequestCommand : IWithdrawRequestCommand
    {
        private readonly IDatabaseService _database;

        public WithdrawRequestCommand(IDatabaseService database)
        {
            _database = database;
        }

        public bool Execute(WithdrawRequestModel model)
        {
            // Null model
            if (model == null)
            {
                return false;
            }

            // Convert currency code to upper (like it is stored in DB)
            model.CurrencyCode = model.CurrencyCode.ToUpper();

            // Request amount incorrect
            if (model.Amount <= 0)
            {
                return false;
            }

            // Wallet not found
            var wallet = _database.Wallets.SingleOrDefault(wallet => wallet.Id == model.WalletId);
            if (wallet == null)
            {
                return false;
            }

            // Wallet found

            // There is no such currency balance in this wallet
            var balance = wallet.Balances.SingleOrDefault(balance => balance.CurrencyCode == model.CurrencyCode);
            if (balance == null)
            {
                return false;
            }

            // Withdraw request amount is greater than wallet currency balance
            if (model.Amount > balance.Amount)
            {
                return false;
            }

            // Request is ok. Everything is ok. Money can be withdrawn.
            balance.Amount -= model.Amount;
            _database.Save();

            // Money are withdrawn at this point.

            // Add transaction entry
            var newTransaction = new Transaction
            {
                Wallet = wallet,
                TransactionType = TransactionType.Withdrawal,
                CurrencyCode = model.CurrencyCode,
                Amount = model.Amount,
                Timestamp = DateTimeOffset.Now
            };
            _database.Trasactions.Add(newTransaction);
            _database.Save();

            return true;
        }
    }
}
