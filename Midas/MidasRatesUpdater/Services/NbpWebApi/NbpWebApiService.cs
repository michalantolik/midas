using MidasRatesUpdater.Services.NbpWebApi.Data;
using System.Text.Json;

namespace MidasRatesUpdater.Services.NbpWebApi
{
    /// <inheritdoc />
    public class NbpWebApiService : INbpWebApiService
    {
        /// <inheritdoc />
        public RatesTableDto GetCurrentExchangeRatesTable(string table)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"http://api.nbp.pl/api/exchangerates/tables/{table}/").Result;
                if (!response.IsSuccessStatusCode)
                {
                    return new RatesTableDto { Rates = new List<RateDto>() };
                }

                var ratesTable = JsonSerializer.Deserialize<List<RatesTableDto>>(response.Content.ReadAsStringAsync().Result);
                if (ratesTable?.SingleOrDefault() == null)
                {
                    return new RatesTableDto { Rates = new List<RateDto>() };
                }

                var result = ratesTable.Single();
                return result;
            }
        }
    }
}
