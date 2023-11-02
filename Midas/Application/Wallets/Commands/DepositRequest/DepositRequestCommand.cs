using Application.Interfaces;
using Domain.Wallets;
using System;
using System.Linq;

namespace Application.Wallets.Commands.DepositRequest
{
    public class DepositRequestCommand : IDepositRequestCommand
    {
        private readonly IDatabaseService _database;

        public DepositRequestCommand(IDatabaseService database)
        {
            _database = database;
        }

        public bool Execute(DepositRequestModel model)
        {
            // Null model
            if (model == null)
            {
                return false;
            }

            // Convert currency code to upper (like it is stored in DB)
            model.CurrencyCode = model.CurrencyCode.ToUpper();

            // Currency not supported
            var availableCurrencies = _database.ExchangeRates.Select(r => r.Code).ToList();
            if (!availableCurrencies.Contains(model.CurrencyCode))
            {
                return false;
            }

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
            var balance = wallet.Balances.SingleOrDefault(balance => balance.CurrencyCode == model.CurrencyCode);
            if (balance == null)
            {
                // Balance does not exist
                var newBalance = new Balance
                {
                    CurrencyCode = model.CurrencyCode,
                    Amount = model.Amount
                };
                wallet.Balances.Add(newBalance);
                _database.Save();
            }
            else
            {
                // Balance exists
                balance.Amount += model.Amount;
                _database.Save();
            }

            // Wallet balance updated

            // Add transaction entry
            var newTransaction = new Transaction
            {
                Wallet = wallet,
                TransactionType = TransactionType.Deposit,
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
