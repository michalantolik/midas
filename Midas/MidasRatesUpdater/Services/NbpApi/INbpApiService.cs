using MidasRatesUpdater.Services.NbpApi.Data;

namespace MidasRatesUpdater.Services.NbpApi
{
    /// <summary>
    /// Service for accessing NBP Web API.
    /// </summary>
    public interface INbpApiService
    {
        /// <summary>
        /// Retrvies current table of exchange rates of type "table".
        /// </summary>
        /// <param name="table">Table type (A, B, or C)</param>
        /// <returns>An instance of <see cref="RatesTableDto"/></returns>
        RatesTableDto GetCurrentExchangeRatesTable(string table);
    }
}
