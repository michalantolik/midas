using Application.Interfaces;
using Data.ExchangeRates;
using Mapping;

namespace Application.ExchangeRates.Commands.UpdateExchangeRates
{
    public class UpdateExchangeRatesCommand : IUpdateExchangeRatesCommand
    {
        private readonly IDatabaseService _database;

        public UpdateExchangeRatesCommand(IDatabaseService database)
        {
            _database = database;
        }

        public void Execute(ExchangeRatesTableDto exchangeRatesTableDto)
        {
            // Map DTOs returned by NBP service into EF Core entities
            var newRates = exchangeRatesTableDto.ToExchangeRates();

            // Delete all existing exchange rates records from DB (we only want to keep the latest ones from Web API)
            var oldRates = _database.ExchangeRates.ToList();
            _database.ExchangeRates.RemoveRange(oldRates);
            _database.Save();

            /// Insert new exchange rates into DB
            _database.ExchangeRates.AddRange(newRates);
            _database.Save();
        }
    }
}
