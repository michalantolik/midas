using Application.Interfaces;
using Domain.Wallets;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Application.Wallets.Commands.WithdrawRequest
{
    public class WithdrawRequestCommand : ControllerBase, IWithdrawRequestCommand
    {
        private readonly IDatabaseService _database;

        public WithdrawRequestCommand(IDatabaseService database)
        {
            _database = database;
        }

        public IActionResult Execute(WithdrawRequestModel model)
        {
            // Null model
            if (model == null)
            {
                return BadRequest($"Request data is null --> You must provide \"{nameof(WithdrawRequestModel)} {nameof(model)}\"");
            }

            // Convert currency code to upper (like it is stored in DB)
            model.CurrencyCode = model.CurrencyCode.ToUpper();

            // Request amount incorrect
            if (model.Amount <= 0)
            {
                return BadRequest($"Withdraw amount is incorrect --> {nameof(model.Amount)}: \"{model.Amount}\" --> Must be greater than 0");
            }

            // Wallet not found
            var wallet = _database.Wallets.SingleOrDefault(wallet => wallet.Id == model.WalletId);
            if (wallet == null)
            {
                return NotFound($"Wallet not found --> {nameof(model.WalletId)}: \"{model.WalletId}\"");
            }

            // Wallet found

            try
            {
                // There is no such currency balance in this wallet
                var balance = wallet.Balances.SingleOrDefault(balance => balance.CurrencyCode == model.CurrencyCode);
                if (balance == null)
                {
                    return BadRequest($"There is no such currency present in this wallet --> {nameof(model.WalletId)}: \"{model.WalletId}\" --> {nameof(model.CurrencyCode)}: \"{model.CurrencyCode}\"");
                }

                // Withdraw request amount is greater than wallet currency balance
                if (model.Amount > balance.Amount)
                {
                    return BadRequest($"You cannot withdraw more money than there is in a wallet --> Requested: \"{model.Amount} {model.CurrencyCode}\" --> Balance: {balance.Amount} \"{balance.CurrencyCode}\""); ;
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

                return Ok($"You withdrawn: {model.Amount} {model.CurrencyCode} \nBalance after withrdawal: {balance.Amount} {balance.CurrencyCode}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error occurred while processing the request --> Message: \"{ex.Message}\"");
            }
        }
    }
}
