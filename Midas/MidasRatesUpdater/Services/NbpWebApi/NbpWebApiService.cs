using MidasRatesUpdater.Data;

namespace MidasRatesUpdater.Services.NbpWebApi
{
    /// <inheritdoc />
    public class NbpWebApiService : INbpWebApiService
    {
        /// <inheritdoc />
        public HttpRepsonseData GetCurrentExchangeRates(string table)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"http://api.nbp.pl/api/exchangerates/tables/{table}/").Result;

                return new HttpRepsonseData
                {
                    Success = response.IsSuccessStatusCode,
                    StatusCode = response.StatusCode,
                    ReasonPhrase = response.ReasonPhrase,
                    Content = response.Content.ReadAsStringAsync().Result
                };
            }
        }
    }
}
