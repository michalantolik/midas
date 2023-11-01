using Data.ExchangeRates;
using Domain.ExchangeRates;

namespace Mapping
{
    /// <summary>
    /// Extension methods used to map DTO objects of type <see cref="ExchangeRatesTableDto"/> into Entity Framework entities.
    /// </summary>
    public static class RatesTableDtoExtensions
    {
        public static List<ExchangeRate> ToExchangeRates(this ExchangeRatesTableDto ratesTable)
        {
            if (ratesTable?.Rates == null)
            {
                return null;
            }


            var exchangeRates = ratesTable.Rates.Select(r => new ExchangeRate
            {
                Currency = r.Currency,
                Code = r.Code,
                Mid = r.Mid,
                TableName = ratesTable.Table,
                TableNo = ratesTable.No,
                EffectiveDate = ratesTable.EffectiveDate
            });

            return exchangeRates.ToList();
        }
    }
}