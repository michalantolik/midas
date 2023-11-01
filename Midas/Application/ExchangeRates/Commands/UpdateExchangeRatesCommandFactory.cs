using Application.Interfaces;

namespace Application.ExchangeRates.Commands
{
    public static class UpdateExchangeRatesCommandFactory
    {
        public static IUpdateExchangeRatesCommand Create(IDatabaseService databaseService)
        {
            return new UpdateExchangeRatesCommand(databaseService);
        }
    }
}
