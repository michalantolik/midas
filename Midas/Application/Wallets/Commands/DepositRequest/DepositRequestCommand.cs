using Application.Interfaces;
using Domain.Wallets;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Application.Wallets.Commands.DepositRequest
{
    public class DepositRequestCommand : ControllerBase, IDepositRequestCommand
    {
        private readonly IDatabaseService _database;

        public DepositRequestCommand(IDatabaseService database)
        {
            _database = database;
        }

        public IActionResult Execute(DepositRequestModel model)
        {
            // Null model
            if (model == null)
            {
                return BadRequest($"Request data is null --> You must provide \"{nameof(DepositRequestModel)} {nameof(model)}\"");
            }

            // Convert currency code to upper (like it is stored in DB)
            model.CurrencyCode = model.CurrencyCode.ToUpper();

            // Currency not supported
            var availableCurrencies = _database.ExchangeRates.Select(r => r.Code).ToList();
            if (!availableCurrencies.Contains(model.CurrencyCode))
            {
                return BadRequest($"Currency is not supported --> {nameof(model.CurrencyCode)}: \"{model.CurrencyCode}\"");
            }

            // Request amount incorrect
            if (model.Amount <= 0)
            {
                return BadRequest($"Deposit amount is incorrect --> {nameof(model.Amount)}: \"{model.Amount}\" --> Must be greater than 0");
            }

            // Wallet not found
            var wallet = _database.Wallets.SingleOrDefault(wallet => wallet.Id == model.WalletId);
            if (wallet == null)
            {
                return NotFound($"Wallet not found --> {nameof(model.WalletId)}: \"{model.WalletId}\"");
            }

            try
            {
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

                return Ok(wallet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error occurred while processing the request --> Message: \"{ex.Message}\"");
            }
        }
    }
}
