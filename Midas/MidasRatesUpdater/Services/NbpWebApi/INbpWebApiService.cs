using MidasRatesUpdater.Data;

namespace MidasRatesUpdater.Services.NbpWebApi
{
    /// <summary>
    /// NBP Web API wrapper.
    /// </summary>
    public interface INbpWebApiService
    {
        /// <summary>
        /// Retrvies current table of exchange rates of type "table".
        /// </summary>
        /// <param name="table">Table type (A, B, or C)</param>
        /// <returns>An instance of <see cref="HttpRepsonseData"/></returns>
        Task<HttpRepsonseData> GetCurrentExchangeRatesAsync(string table);
    }
}
