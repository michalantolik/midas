using Application.Interfaces;
using Domain.ExchangeRates;

namespace Application.ExchangeRates.Commands.Queries
{
    public class GetExchangeRatesQuery : IGetExchangeRatesQuery
    {
        private readonly IDatabaseService _database;

        public GetExchangeRatesQuery(IDatabaseService database)
        {
            _database = database;
        }

        public List<ExchangeRateModel> Execute()
        {
            var exchangeRates = _database.ExchangeRates.Select(rate => new ExchangeRateModel
            {
                Id = rate.Id,
                Currency = rate.Currency,
                Code = rate.Code,
                Mid = rate.Mid,
                TableName = rate.TableName,
                TableNo = rate.TableNo,
                EffectiveDate = rate.EffectiveDate,
                CreatedDate = rate.CreatedDate,
            }).ToList();

            return exchangeRates;
        }
    }
}
