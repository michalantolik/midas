using Application.Interfaces;

namespace Application.CurrencyConversion
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private readonly IDatabaseService _database;

        public CurrencyConverter(IDatabaseService database)
        {
            _database = database;
        }

        public bool TryConvert(
            string sourceCurrencyCode,
            string targetCurrencyCode,
            decimal sourceAmount,
            out decimal targetAmount)
        {
            // Currencies not supported (not present in exchange rates table)
            var availableCurrencies = _database.ExchangeRates.Select(r => r.Code).ToList();
            if (!availableCurrencies.Contains(sourceCurrencyCode) || !availableCurrencies.Contains(targetCurrencyCode))
            {
                targetAmount = 0;
                return false;
            }

            // Get source to PLN exchange rate
            decimal sourceToPlnRate = _database.ExchangeRates
                .Where(r => r.Code == sourceCurrencyCode)
                .Select(r => r.Mid)
                .First();

            // Get target to PLN exchange rate
            decimal targetToPlnRate = _database.ExchangeRates
                .Where(r => r.Code == targetCurrencyCode)
                .Select(r => r.Mid)
                .First();

            // Convert source currency to PLN
            decimal plnAmount = sourceAmount * sourceToPlnRate;

            // Convert PLN to target currency
            targetAmount = plnAmount / targetToPlnRate;

            return true;
        }
    }
}
