using Application.CurrencyConversion;
using Application.Interfaces;
using Domain.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace Application.Wallets.Commands.ConvertRequest
{
    public class ConvertRequestCommand : ControllerBase, IConvertRequestCommand
    {
        private readonly IDatabaseService _database;
        private readonly ICurrencyConverter _currencyConverter;

        public ConvertRequestCommand(
            IDatabaseService database,
            ICurrencyConverter currencyConverter)
        {
            _database = database;
            _currencyConverter = currencyConverter;
        }

        public IActionResult Execute(ConvertRequestModel model)
        {
            // Null model
            if (model == null)
            {
                return BadRequest($"Request data is null --> You must provide \"{nameof(ConvertRequestModel)} {nameof(model)}\"");
            }

            // Convert currency code to upper (like it is stored in DB)
            model.SourceCurrencyCode = model.SourceCurrencyCode.ToUpper();
            model.TargetCurrencyCode = model.TargetCurrencyCode.ToUpper();

            // Request amount incorrect
            if (model.SourceAmountToConvert <= 0)
            {
                return BadRequest($"Convert amount is incorrect --> {nameof(model.SourceAmountToConvert)}: \"{model.SourceAmountToConvert}\" --> Must be greater than 0");
            }

            // Currency not supported (not present in exchange rates table)
            var availableCurrencies = _database.ExchangeRates.Select(r => r.Code).ToList();
            if (!availableCurrencies.Contains(model.SourceCurrencyCode))
            {
                return BadRequest($"Source Currency is not supported --> {nameof(model.SourceCurrencyCode)}: \"{model.SourceCurrencyCode}\" --> No exchange rate data present");
            }
            if (!availableCurrencies.Contains(model.TargetCurrencyCode))
            {
                return BadRequest($"Target Currency is not supported --> {nameof(model.TargetCurrencyCode)}: \"{model.TargetCurrencyCode}\" --> No exchange rate data present");
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
                // There is no such source currency balance in this wallet
                var sourceBalance = wallet.Balances.SingleOrDefault(balance => balance.CurrencyCode == model.SourceCurrencyCode);
                if (sourceBalance == null)
                {
                    return BadRequest($"There is no such source currency present in this wallet --> {nameof(model.WalletId)}: \"{model.WalletId}\" --> {nameof(model.SourceCurrencyCode)}: \"{model.SourceCurrencyCode}\"");
                }

                // Convert request source amount is greater than wallet source currency balance
                if (model.SourceAmountToConvert > sourceBalance.Amount)
                {
                    return BadRequest($"You cannot convert more money than there is in a wallet --> Requested: \"{model.SourceAmountToConvert} {model.SourceCurrencyCode}\" --> Balance: {sourceBalance.Amount} \"{sourceBalance.CurrencyCode}\""); ;
                }

                // Try to convert currency (it should be possible at this point)
                if (!_currencyConverter.TryConvert(
                    model.SourceCurrencyCode,
                    model.TargetCurrencyCode,
                    model.SourceAmountToConvert,
                    out var targetAmount))
                {
                    return StatusCode(500, $"Server error occurred while processing the request --> Currency conversion failed");
                }

                // Request is ok. Everything is ok. Conversion transaction can be done.
                sourceBalance.Amount -= model.SourceAmountToConvert;

                var targetBalance = wallet.Balances.SingleOrDefault(balance => balance.CurrencyCode == model.TargetCurrencyCode);
                if (targetBalance == null)
                {
                    // Target Balance does not exist
                    var newTargetBalance = new Balance
                    {
                        CurrencyCode = model.TargetCurrencyCode,
                        Amount = targetAmount
                    };
                    wallet.Balances.Add(newTargetBalance);
                }
                else
                {
                    // Balance exists
                    targetBalance.Amount += targetAmount;
                }
                _database.Save();

                // Money are converted at this point.

                // Add transaction entry
                var newTransaction = new Transaction
                {
                    Wallet = wallet,
                    TransactionType = TransactionType.Conversion,
                    CurrencyCode = model.SourceCurrencyCode,
                    TargetCurrencyCode = model.TargetCurrencyCode,
                    Amount = model.SourceAmountToConvert,
                    TargetAmount = targetAmount,
                    Timestamp = DateTimeOffset.Now
                };
                _database.Trasactions.Add(newTransaction);
                _database.Save();

                targetBalance = wallet.Balances.Single(b => b.CurrencyCode == model.TargetCurrencyCode);

                return Ok($"You converted: {model.SourceAmountToConvert} {model.SourceCurrencyCode} to {targetAmount} {model.TargetCurrencyCode}\nSource balance after conversion: {sourceBalance.Amount} {sourceBalance.CurrencyCode}\nTarget balance after conversion: {targetBalance.Amount} {targetBalance.CurrencyCode}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error occurred while processing the request --> Message: \"{ex.Message}\"");
            }
        }
    }
}
