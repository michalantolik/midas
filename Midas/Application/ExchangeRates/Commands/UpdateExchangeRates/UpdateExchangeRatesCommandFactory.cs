using Application.Interfaces;

namespace Application.ExchangeRates.Commands.UpdateExchangeRates
{
    public static class UpdateExchangeRatesCommandFactory
    {
        public static IUpdateExchangeRatesCommand Create(IDatabaseService databaseService)
        {
            return new UpdateExchangeRatesCommand(databaseService);
        }
    }
}
