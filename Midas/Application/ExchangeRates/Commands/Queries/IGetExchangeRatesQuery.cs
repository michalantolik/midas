using Domain.ExchangeRates;

namespace Application.ExchangeRates.Commands.Queries
{
    public interface IGetExchangeRatesQuery
    {
        List<ExchangeRateModel> Execute();
    }
}
