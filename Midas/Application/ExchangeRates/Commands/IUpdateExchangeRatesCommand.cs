using Data.ExchangeRates;

namespace Application.ExchangeRates.Commands
{
    /// <summary>
    /// Interface for updating ExchangeRates table in the Database.
    /// </summary>
    public interface IUpdateExchangeRatesCommand
    {
        /// <summary>
        /// Updates ExchangeRates table in the Database.
        /// </summary>
        /// <param name="exchangeRatesTableDto">New (updated) exchange rates to be persisted.</param>
        void Execute(ExchangeRatesTableDto exchangeRatesTableDto);
    }
}