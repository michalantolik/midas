using MidasRatesUpdater.Data;

namespace MidasRatesUpdater.Services
{
    /// <inheritdoc />
    public class NbpWebApiWrapper : INbpWebApiWrapper
    {
        /// <inheritdoc />
        public async Task<HttpRepsonseData> GetCurrentExchangeRatesAsync(string table)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"http://api.nbp.pl/api/exchangerates/tables/{table}/");

                return new HttpRepsonseData
                {
                    Success = response.IsSuccessStatusCode,
                    StatusCode = response.StatusCode,
                    ReasonPhrase = response.ReasonPhrase,
                    Content = await response.Content.ReadAsStringAsync()
                };
            }
        }
    }
}
