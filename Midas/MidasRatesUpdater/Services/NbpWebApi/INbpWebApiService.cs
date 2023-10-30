using MidasRatesUpdater.Services.NbpWebApi.Data;

namespace MidasRatesUpdater.Services.NbpWebApi
{
    /// <summary>
    /// Service for accessing NBP Web API.
    /// </summary>
    public interface INbpWebApiService
    {
        /// <summary>
        /// Retrvies current table of exchange rates of type "table".
        /// </summary>
        /// <param name="table">Table type (A, B, or C)</param>
        /// <returns>An instance of <see cref="RatesTableDto"/></returns>
        RatesTableDto GetCurrentExchangeRatesTable(string table);
    }
}
