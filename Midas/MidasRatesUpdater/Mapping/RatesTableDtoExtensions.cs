using MidasRatesUpdater.Services.Database.Entities;
using MidasRatesUpdater.Services.NbpApi.Data;

namespace MidasRatesUpdater.Mapping
{
    /// <summary>
    /// Extension methods used to map DTO objects of type <see cref="RatesTableDto"/> into Entity Framework entities.
    /// </summary>
    public static class RatesTableDtoExtensions
    {
        public static List<ExchangeRate> ToExchangeRates(this RatesTableDto ratesTable)
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
